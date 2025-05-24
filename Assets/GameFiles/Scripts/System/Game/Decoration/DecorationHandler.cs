using System;
using UnityEngine;

public class DecorationHandler : Singleton<DecorationHandler>
{
    GameObject bird;
    
    private void Start()
    {
        bird = PoolingHandler.Instance.Spawn(DecorationType.Bird, transform.position, Quaternion.identity);
    }
}
