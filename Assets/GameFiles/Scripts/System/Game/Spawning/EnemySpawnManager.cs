using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Burst;
using UnityEngine;

[BurstCompile]
public class EnemySpawnManager : SpawningManager
{
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private ParticleSystem despawnEffect;
    
    [Space(7)]
    [SerializeField] private Vector2 spawnRangeY;
    [SerializeField] private Vector2 spawnTimeRange;
    [SerializeField] private float cameraDistance;
    
    [Space(7)]
    [SerializeField] private PoolingHandler poolingHandler;
    
    // caching
    private GameObject _object;
    private float _rndTime;
    private int _lifeTime;
    private Vector3 _position;

    protected override async void Spawn(CancellationToken cancelToken)
    {
        while (!cancelToken.IsCancellationRequested)
        {
            _rndTime = await RandomNumber(spawnTimeRange.x, spawnTimeRange.y)*1000;
            await Task.Delay((int)_rndTime, cancelToken);
            
            // Fix error with get_position
            // Note: can't use Unity tools here, cause of multithreading
            // Another Note: use rider debugger for every single test
            _position = new Vector3(cameraPosition.position.x + cameraDistance, cameraPosition.position.y + await RandomNumber(spawnRangeY.x, spawnRangeY.y), 0);
            _object = poolingHandler.Spawn(_position, Quaternion.identity);
            
            await Despawn(cancelToken, _object);
        }
    }

    protected override async Task<int> Despawn(CancellationToken cancelToken, GameObject go)
    {
        if (TokenSource.IsCancellationRequested) return 0;
        
        go.TryGetComponent(out ISpawnable spawnable);
        _lifeTime = (int)(spawnable.LifeTime * 1000);
        await Task.Delay(_lifeTime, cancelToken);
        
        if (despawnEffect)
            ParticleHandler.Instance.SpawnParticles(despawnEffect, go.transform.position, go.transform.rotation);
        poolingHandler.Despawn(go);

        return 0;
    }
    
    protected override Task<int> Despawn(CancellationToken cancelToken, GameObject go, int poolingIndex) { throw new NotImplementedException(); }
}