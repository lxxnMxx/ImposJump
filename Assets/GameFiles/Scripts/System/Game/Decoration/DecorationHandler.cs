using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class DecorationHandler : Singleton<DecorationHandler>
{
    [SerializeField] private Transform cameraPosition;
    [Space(5)]
    [SerializeField] private Vector2 spawnRangeY;
    [SerializeField] private Vector2 spawnTimeRange;
    
    // Initializing (making the Cpu comfortable with this variable)
    private GameObject _object;
    private float rnd;
    private int rndType;
    private Vector3 position;
    
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Level1")
            StartCoroutine(SpawnDecorationLvl1());
    }

    private IEnumerator SpawnDecorationLvl1()
    {
        while (true)
        {
            rnd = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            yield return new WaitForSeconds(rnd);
            position = new Vector3(cameraPosition.position.x + 20, cameraPosition.position.y + Random.Range(spawnRangeY.x, spawnRangeY.y), 0);
            _object = PoolingHandler.Instance.Spawn(DecorationType.Bird, position, Quaternion.identity);
            StartCoroutine(Despawn(_object));
        }
    }
    
    private IEnumerator Despawn(GameObject go)
    {
        yield return new WaitForSeconds(10);
        PoolingHandler.Instance.Despawn(go);
    }
}
