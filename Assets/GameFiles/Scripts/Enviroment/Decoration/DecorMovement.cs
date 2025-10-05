using System;
using System.Collections;
using UnityEngine;

public class DecorMovement : MonoBehaviour, ISpawnable
{
    [Header("ISpawnable")]
    [SerializeField] private float lifeTime;
    public float LifeTime => lifeTime;
    

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