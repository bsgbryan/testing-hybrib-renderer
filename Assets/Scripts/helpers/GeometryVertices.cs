using Unity.Entities;
using Unity.Collections;

struct Vertexes {
  public BlobArray<ushort> Elements;
}

public struct GeometryVertices {
  private static BlobAssetReference<Vertexes> vertexes;

  public static void Hydrate() {
    using (var builder = new BlobBuilder(Allocator.Temp)) {
      ref var root = ref builder.ConstructRoot<Vertexes>();
      var vertexArray = builder.Allocate(ref root.Elements, 20);
    
      //                   0, 1, 0, 2, 0, 3
      vertexArray[ 0] = 0b00_01_00_10_00_11_0000;
      //                   1, 0, 1, 3, 1, 2
      vertexArray[ 1] = 0b01_00_01_11_01_10_0000;
      //                   0, 3, 1, 2, 0, 2 PAIRED
      vertexArray[ 2] = 0b00_11_01_10_00_10_0000;
      //                   0, 3, 1, 3, 1, 2 PAIRED
      vertexArray[ 3] = 0b00_11_01_11_01_10_0000;
      //                   2, 0, 2, 1, 2, 3
      vertexArray[ 4] = 0b10_00_10_01_10_11_0000;
      //                   0, 1, 2, 3, 0, 3 PAIRED
      vertexArray[ 5] = 0b00_01_10_11_00_11_0000;
      //                   0, 1, 1, 2, 2, 3 PAIRED
      vertexArray[ 6] = 0b00_01_01_10_10_11_0000;
      //                   0, 1, 1, 3, 2, 3 PAIRED
      vertexArray[ 7] = 0b00_01_01_11_10_11_0000;
      //                   0, 1, 2, 3, 0, 2 PAIRED
      vertexArray[ 8] = 0b00_01_10_11_00_10_0000;
      //                   3, 0, 3, 1, 3, 2
      vertexArray[ 9] = 0b11_00_11_01_11_10_0000;
      //                   3, 0, 3, 2, 3, 1
      vertexArray[10] = 0b11_00_11_10_11_01_0000;
      //                   0, 1, 2, 3, 1, 3 PAIRED
      vertexArray[11] = 0b00_01_10_11_01_11_0000;
      //                   0, 1, 0, 2, 2, 3 PAIRED
      vertexArray[12] = 0b00_01_00_10_10_11_0000;
      //                   0, 1, 0, 3, 2, 3 PAIRED
      vertexArray[13] = 0b00_01_00_11_10_11_0000;
      //                   0, 1, 2, 3, 1, 2 PAIRED
      vertexArray[14] = 0b00_01_10_11_01_10_0000;
      //                   2, 0, 2, 3, 2, 1
      vertexArray[15] = 0b10_00_10_11_10_01_0000;
      //                   0, 3, 0, 2, 1, 2 PAIRED
      vertexArray[16] = 0b00_11_00_10_01_10_0000;
      //                   0, 3, 1, 2, 1, 3 PAIRED
      vertexArray[17] = 0b00_11_01_10_01_11_0000;
      //                   1, 0, 1, 2, 1, 3
      vertexArray[18] = 0b01_00_01_10_01_11_0000;
      //                   0, 1, 0, 3, 0, 2
      vertexArray[19] = 0b00_01_00_11_00_10_0000;

      vertexes = builder.CreateBlobAssetReference<Vertexes>(Allocator.Persistent);
    }
  }

  /*
    The last four bits (lowest) are ignored for now.
    It sucks, but it's better than using a 32 bit struct
    for values that will never need more than 2 bits each.
    Hopefully, I'll be able to find a use for the 4 dangling
    bits someday ...
   */
  public static ushort vertex(int element, ushort index) {
    ushort indexOffset   = (ushort) (index * 0b0000_0000_0000_0010);
    ushort mask          = (ushort) (0b1100_0000_0000_0000 >> indexOffset);
    ushort maskedValue   = (ushort) (vertexes.Value.Elements[element] & mask);
    ushort shiftDistance = (ushort) (0b0000_0000_0000_1110 - indexOffset);
    
    return (ushort) (maskedValue >> shiftDistance);
  }
}