using System;
using System.Collections;
using UnityEngine;

public class DecorMovement : MonoBehaviour, ISpawnable
{
    [Header("ISpawnable")]
    [SerializeField] private float lifeTime;
    [SerializeField] private Vector2 spawnTimeRange;
    public float LifeTime => lifeTime;
    public Vector2 SpawnTimeRange => spawnTimeRange;
    

    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    private ISpawnable _spawnableImplementation;

    private void OnEnable()
    {
        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        while (true)
        {
            transform.position += direction * speed * Time.deltaTime;
            yield return null;
        }
    }
}