using UnityEngine;

public interface ISpawnable 
{
    public float LifeTime { get; }
    public Vector2 SpawnTimeRange { get; }
}