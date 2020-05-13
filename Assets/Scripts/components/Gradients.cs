using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;

public struct GradientElements {
  public BlobArray<float3> Elements;
}

public struct Gradients : IComponentData {
  private static byte[] Table = {
		151,160,137, 91, 90, 15,131, 13,201, 95, 96, 53,194,233,  7,225,
		140, 36,103, 30, 69,142,  8, 99, 37,240, 21, 10, 23,190,  6,148,
		247,120,234, 75,  0, 26,197, 62, 94,252,219,203,117, 35, 11, 32,
		 57,177, 33, 88,237,149, 56, 87,174, 20,125,136,171,168, 68,175,
		 74,165, 71,134,139, 48, 27,166, 77,146,158,231, 83,111,229,122,
		 60,211,133,230,220,105, 92, 41, 55, 46,245, 40,244,102,143, 54,
		 65, 25, 63,161,  1,216, 80, 73,209, 76,132,187,208, 89, 18,169,
		200,196,135,130,116,188,159, 86,164,100,109,198,173,186,  3, 64,
		 52,217,226,250,124,123,  5,202, 38,147,118,126,255, 82, 85,212,
		207,206, 59,227, 47, 16, 58, 17,182,189, 28, 42,223,183,170,213,
		119,248,152,  2, 44,154,163, 70,221,153,101,155,167, 43,172,  9,
		129, 22, 39,253, 19, 98,108,110, 79,113,224,232,178,185,112,104,
		218,246, 97,228,251, 34,242,193,238,210,144, 12,191,179,162,241,
		 81, 51,145,235,249, 14,239,107, 49,192,214, 31,181,199,106,157,
		184, 84,204,176,115,121, 50, 45,127,  4,150,254,138,236,205, 93,
		222,114, 67, 29, 24, 72,243,141,128,195, 78, 66,215, 61,156,180
	};

  public static BlobAssetReference<GradientElements> Entries;

  public static void Hydrate() {
    using (var builder = new BlobBuilder(Allocator.Temp)) {
      ref var root = ref builder.ConstructRoot<GradientElements>();
      var gradients = builder.Allocate(ref root.Elements, 32);

      var g = 0.5773503f;
      
      var pp_ = new float3( g,  g, 0f);
      var np_ = new float3(-g,  g, 0f);
      var pn_ = new float3( g, -g, 0f);
      var nn_ = new float3(-g, -g, 0f);
      
      var p_p = new float3( g, 0f,  g);
      var n_p = new float3(-g, 0f,  g);
      var p_n = new float3( g, 0f, -g);
      var n_n = new float3(-g, 0f, -g);

      var _pp = new float3( 0f,  g,  g);
      var _np = new float3( 0f, -g,  g);
      var _pn = new float3( 0f,  g, -g);
      var _nn = new float3( 0f, -g, -g);

      gradients[ 0] = pp_;
      gradients[ 1] = np_;
      gradients[ 2] = pn_;
      gradients[ 3] = nn_;
      
      gradients[ 4] = p_p;
      gradients[ 5] = n_p;
      gradients[ 6] = p_n;
      gradients[ 7] = n_n;
      
      gradients[ 8] = _pp;
      gradients[ 9] = _np;
      gradients[10] = _pn;
      gradients[11] = _nn;
      
      gradients[12] = gradients[ 0];
      gradients[13] = gradients[ 1];
      gradients[14] = gradients[ 2];
      gradients[15] = gradients[ 3];
      
      gradients[16] = gradients[ 4];
      gradients[17] = gradients[ 5];
      gradients[18] = gradients[ 6];
      gradients[19] = gradients[ 7];
      
      gradients[20] = gradients[ 8];
      gradients[21] = gradients[ 9];
      gradients[22] = gradients[10];
      gradients[23] = gradients[11];
      
      var ppp = new float3( g,  g, g);
      var npp = new float3(-g,  g, g);
      var pnp = new float3( g, -g, g);
      var nnp = new float3(-g, -g, g);

      gradients[24] = ppp;
      gradients[25] = npp;
      gradients[26] = pnp;
      gradients[27] = nnp;

      var ppn = new float3( g,  g, -g);
      var npn = new float3(-g,  g, -g);
      var pnn = new float3( g, -g, -g);
      var nnn = new float3(-g, -g, -g);
      
      gradients[28] = ppn;
      gradients[29] = npn;
      gradients[30] = pnn;
      gradients[31] = nnn;

      Entries = builder.CreateBlobAssetReference<GradientElements>(Allocator.Persistent);
    }
  }
}