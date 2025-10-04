using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Burst;
using UnityEngine;
using Random = UnityEngine.Random;

[BurstCompile]
public class DecorationHandler : SpawningManager
{
    [SerializeField] private Transform cameraPosition;
    
    [Space(7)]
    [SerializeField] private Vector2 spawnRangeY;
    [SerializeField] private Vector2 spawnTimeRange;
    [SerializeField] private float cameraDistance;
    
    [Space(7)]
    [SerializeField] private List<PoolingHandler> poolingHandlers;
    
    // caching
    private GameObject _object;
    private float _rndTime;
    private int _rndPooling;
    private int _rndType;
    private Vector3 _position;


    protected override async void Spawn(CancellationToken cancelToken)
    {
        while (cancelToken.IsCancellationRequested)
        {
            _rndTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            _rndPooling = Random.Range(0, poolingHandlers.Count);
            
            await Task.Run(() => Countdown(cancelToken, _rndTime));
            _position = new Vector3(cameraPosition.position.x + cameraDistance, cameraPosition.position.y + Random.Range(spawnRangeY.x, spawnRangeY.y), 0);
            _object = poolingHandlers[_rndPooling].Spawn(_position, Quaternion.identity);
            
            await Task.Run(() => Despawn(cancelToken, _object, _rndPooling));
        }
    }

    protected override async Task<int> Despawn(CancellationToken cancelToken, GameObject go, int poolingIndex)
    {
        if (TokenSource.IsCancellationRequested) return 0;
        
        go.TryGetComponent(out ISpawnable spawnable);
        await Task.Run(() => Countdown(cancelToken, spawnable.LifeTime));
        
        poolingHandlers[poolingIndex].Despawn(go);
        return 0;
    }

    protected override Task<int> Despawn(CancellationToken cancelToken, GameObject go) { throw new NotImplementedException(); }
}