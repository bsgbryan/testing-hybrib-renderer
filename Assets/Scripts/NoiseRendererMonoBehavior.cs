using UnityEngine;

using Unity.Mathematics;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;

public class NoiseRendererMonoBehavior : MonoBehaviour {
  public float3 resolution, scale;
  public float minimum, frequency;
  public int3 Chunks = new int3(1, 1, 1);

  private void Start() {
    EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

    EntityArchetype volumetricNoise = entityManager.CreateArchetype(
      typeof(Resolution),
      typeof(Scale),
      typeof(Min),
      typeof(Frequency),
      typeof(LocalToWorld),
      typeof(RenderMesh)
    );

    NativeArray<Entity> entities = new NativeArray<Entity>(100, Allocator.Temp);

    entityManager.CreateEntity(volumetricNoise, entities);

    var res  = new Resolution { Value = resolution };
    var scal = new Scale      { Value = scale      };
    var min  = new Min        { Value = minimum    };
    var freq = new Frequency  { Value = frequency  };

    GeometryVertices.Hydrate();
    SampleIndexes.Hydrate();
    PermutationTable.Hydrate();
    Gradients.Hydrate();

    for(int i = 0; i < entities.Length; i++) {
      Entity entity = entities[i];

      entityManager.SetComponentData(entity, res );
      entityManager.SetComponentData(entity, scal);
      entityManager.SetComponentData(entity, min );
      entityManager.SetComponentData(entity, freq);
    }
  }

  // private void Update() {
    // if (NeedsRender == true) {
    //   NeedsRender = false;
      
    //   var triangles = new NativeList<int>    (Allocator.Persistent);
    //   var vertices  = new NativeList<Vector3>(Allocator.Persistent);

    //   // var job = new NoiseRenderJob {
    //   var Renderer = new NoiseRenderer {
    //       Resolution = Resolution,
    //       Scale      = Scale,
    //       Chunks     = Chunks,
    //       Min        = Min,
    //       Frequency  = Frequency,
    //       Triangles  = triangles,
    //       Vertices   = vertices,
    //       GeometryVertices   = geometryVertices,
    //       GeometryVertexMask = geometryVertexMask,
    //       SampleIndexes      = sampleIndexes,
    //       Table     = table,
    //       Gradients = gradients
    //     };
    //   // };

    //   // int instances = (int) (Chunks.x * Chunks.y * Chunks.z);

    //   // job.Schedule(instances, 1).Complete();
      
    //   var mesh = new Mesh();

    //   mesh.vertices  = vertices.ToArray();
    //   mesh.triangles = triangles.ToArray();

    //   mesh.RecalculateNormals();

    //   // GetComponent<MeshFilter>().sharedMesh = mesh;

    //   triangles.Dispose();
    //   vertices.Dispose();
    // }
  // }
}
