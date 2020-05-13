using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;

public struct GradientElements {
  public BlobArray<float3> Elements;
}

public struct Gradients {
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