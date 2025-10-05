using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Burst;
using UnityEngine;
using Random = UnityEngine.Random;

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

    protected override async Task<int> Spawn(CancellationToken cancelToken)
    {
        while (!cancelToken.IsCancellationRequested)
        {
            _rndTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            try
            {
                await Countdown(cancelToken, _rndTime);
            }
            catch (TaskCanceledException e)
            {
                print("TaskCanceledException");
                return 0;
            }
            
            _position = new Vector3(cameraPosition.position.x + cameraDistance, cameraPosition.position.y + Random.Range(spawnRangeY.x, spawnRangeY.y), 0);
            _object = await poolingHandler.Spawn(_position, Quaternion.identity);
            
            await Despawn(cancelToken, _object);
        }

        return 0;
    }

    protected override async Task<int> Despawn(CancellationToken cancelToken, GameObject go)
    {
        if (TokenSource.IsCancellationRequested) return 0;
        
        go.TryGetComponent(out ISpawnable spawnable);

        try
        {
            await Countdown(cancelToken, spawnable.LifeTime);
        }
        catch (TaskCanceledException e)
        {
            print("TaskCanceledException");
        }
        
        if (despawnEffect)
            ParticleHandler.Instance.SpawnParticles(despawnEffect, go.transform.position, go.transform.rotation);
        poolingHandler.Despawn(go);

        return 0;
    }
    
    protected override Task<int> Despawn(CancellationToken cancelToken, GameObject go, int poolingIndex) { throw new NotImplementedException(); }
}