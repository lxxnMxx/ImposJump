using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using UnityEngine;

// this struct is for the identification of the Entities ln. 13 
// also it needs to be added as the object gets created
//public struct Decoration: IComponentData { }

[BurstCompile]
public partial class DecorationSystem : SystemBase
{
    [BurstCompile]
    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
        
        Entities.ForEach((ref LocalTransform transform, ref DecorationData data) =>
        {
            transform.Position += data.direction * data.velocity * deltaTime;
            Debug.Log("DecorationSystem!!!!");
        }).ScheduleParallel();
    }
}