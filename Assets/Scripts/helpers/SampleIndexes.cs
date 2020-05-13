using Unity.Entities;
using Unity.Collections;

struct Indexes {
  public BlobArray<ushort> Elements;
}

public struct SampleIndexes {
  private static BlobAssetReference<Indexes> indexes;

    public static void Hydrate() {
    using (var builder = new BlobBuilder(Allocator.Temp)) {
      ref var root = ref builder.ConstructRoot<Indexes>();
      var indexArray = builder.Allocate(ref root.Elements, 6);
    
      //                  0,  2,  3,  7
      indexArray[0] = 0b000_010_011_111_000;
      //                  0,  2,  7,  6
      indexArray[1] = 0b000_010_111_110_000;
      //                  0,  4,  6,  7
      indexArray[2] = 0b000_100_110_111_000;
      //                  0,  6,  1,  2
      indexArray[3] = 0b000_110_001_010_000;
      //                  0,  4,  1,  6
      indexArray[4] = 0b000_100_001_110_000;
      //                  5,  6,  1,  4
      indexArray[5] = 0b101_110_001_100_000;

      indexes = builder.CreateBlobAssetReference<Indexes>(Allocator.Persistent);
    }
  }


  /*
    The last four bits (lowest) are ignored for now.
    It sucks, but it's better than using a 32 bit struct
    for values that will never need more than 3 bits each.
    Hopefully, I'll be able to find a use for the 4 dangling
    bits someday ...
   */
  public static ushort index(ushort element, ushort index) {
    ushort indexOffset   = (ushort) (index * 0b0000_0000_0000_0011);
    ushort mask          = (ushort) (0b1110_0000_0000_0000 >> indexOffset);
    ushort maskedValue   = (ushort) (indexes.Value.Elements[element] & mask);
    ushort shiftDistance = (ushort) (0b0000_0000_0000_1101 - indexOffset);
    
    return (ushort) (maskedValue >> shiftDistance);
  }
}