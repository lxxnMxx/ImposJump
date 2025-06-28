using System;
using System.Collections;
using UnityEngine;

public class AlienMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    [SerializeField] private Vector2 distanceToPlayer;
    [SerializeField] private float speed;


    private void OnEnable()
    {
        SoundManager.Instance.Play(SoundList.Alien, SoundType.AliensComing);
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
