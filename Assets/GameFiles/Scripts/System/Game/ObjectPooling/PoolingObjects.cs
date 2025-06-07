using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolingObjects : MonoBehaviour
{
    public static PoolingObjects Instance {get; private set;}
    
    [Header("======== Level 1 ========")]
    public List<GameObject> birdPool;

    private void Awake() => Instance = this;
}