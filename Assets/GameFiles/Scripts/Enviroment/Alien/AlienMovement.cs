using System;
using System.Collections;
using UnityEngine;

public enum EnemyType
{
    Alien,
    Cloud
}

public class AlienMovement : MonoBehaviour, ISpawnable
{
    [Header("ISpawnable")]
    [SerializeField] private float lifeTime;
    [SerializeField] private Vector2 spawnTimeRange;
    public float LifeTime => lifeTime;
    public Vector2 SpawnTimeRange => spawnTimeRange;

    [SerializeField] private EnemyType type;
    [SerializeField] private Transform target;
    
    [SerializeField] private Vector2 distanceToPlayer;
    [SerializeField] private float speed;


    private void OnEnable()
    {
        switch (type) 
        {
            case EnemyType.Alien: SoundManager.Instance.Play(SoundList.Alien, SoundType.AliensComing);
                break;
            case EnemyType.Cloud:
                SoundManager.Instance.Play(SoundList.BadCloud, SoundType.BadCloudsComing);
                break;
            default: 
                print("Type not found"); break;
        }
        
        StartCoroutine(Movement());
    }
    
    IEnumerator Movement()
    {
        while (true)
        {
            if (Vector3.Distance(target.position, transform.position) < distanceToPlayer.x || Vector3.Distance(target.position, transform.position) > distanceToPlayer.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position + new Vector3(distanceToPlayer.x, distanceToPlayer.y, 0), Time.deltaTime * speed);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
