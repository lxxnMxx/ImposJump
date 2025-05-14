using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class DecorationAuthoring : MonoBehaviour
{
    public float velocity;
    public float3 direction;
}

class Baker : Baker<DecorationAuthoring>
{
    public override void Bake(DecorationAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        
        AddComponent(entity, new DecorationData()
        {
            velocity = authoring.velocity,
            direction = authoring.direction
        });
    }
}
