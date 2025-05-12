using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Platform Settings")] 
    [SerializeField] Transform[] points;
    [SerializeField] int startingIndex;
    
    [SerializeField] float speed;

    private int _i;
    
    void Start()
    {
        transform.position = points[startingIndex].position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, points[_i].position) <= 0.02f)
        {
            _i += 1;
            if (_i == points.Length) _i = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, points[_i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if(transform.position.y < other.transform.position.y-0.5)
            other.transform.SetParent(gameObject.transform);

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
