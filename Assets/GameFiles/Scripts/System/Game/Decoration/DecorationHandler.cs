using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DecorationHandler : Singleton<DecorationHandler>
{
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private ParticleSystem despawnEffect;
    
    [Space(7)]
    [SerializeField] private Vector2 spawnRangeY;
    [SerializeField] private Vector2 spawnTimeRange;
    [SerializeField] private float cameraDistance; // standard is 20
    
    [Space(7)]
    [SerializeField] private List<PoolingHandler> poolingHandlers;

    [Space(7)]
    [SerializeField] private float lifeTime;
    
    // Initializing (making the Cpu comfortable with this variable)
    private GameObject _object;
    private float rnd;
    private int rndPooling;
    private int rndType;
    private Vector3 position;
    
    private ParticleSystem _ps;
    private ParticleSystem _component;
    
    private void Start()
    {
        if(SceneHandler.Instance.IsCurrentSceneLevel())
            StartCoroutine(SpawnDecoration());
    }

    private IEnumerator SpawnDecoration()
    {
        while (true)
        {
            rnd = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            rndPooling = Random.Range(0, poolingHandlers.Count);
            yield return new WaitForSeconds(rnd);
            position = new Vector3(cameraPosition.position.x + cameraDistance, cameraPosition.position.y + Random.Range(spawnRangeY.x, spawnRangeY.y), 0);
            _object = poolingHandlers[rndPooling].Spawn(position, Quaternion.identity);
            StartCoroutine(Despawn(_object, rndPooling));
        }
    }
    
    private IEnumerator Despawn(GameObject go, int poolingIndex)
    {
        yield return new WaitForSeconds(lifeTime);
        if (despawnEffect != null)
        {
            _ps = Instantiate(despawnEffect, go.transform.position, go.transform.rotation);
            _component = _ps.gameObject.GetComponent<ParticleSystem>();
            Destroy(_ps.gameObject, _component.main.duration + _component.startLifetime);   
        }
        poolingHandlers[poolingIndex].Despawn(go);
    }
}
