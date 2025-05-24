using System;
using UnityEngine;

public class DecorationHandler : Singleton<DecorationHandler>
{
    [SerializeField] private Transform cameraPosition;
    
    GameObject bird;
    
    private void Start()
    {
        // spawn like this
        var position = new Vector3(cameraPosition.position.x+20, cameraPosition.position.y+3, 0);
        bird = PoolingHandler.Instance.Spawn(DecorationType.Bird, position, Quaternion.identity);
    }
}
