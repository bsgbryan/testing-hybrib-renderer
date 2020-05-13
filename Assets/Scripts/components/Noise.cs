using Unity.Entities;
using Unity.Mathematics;

public struct Noise : IComponentData {
  public float Frequency, Min;
  public float3 Resolution, Scale;
}