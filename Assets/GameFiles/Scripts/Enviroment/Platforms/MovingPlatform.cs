using System;
using System.Collections;
using UnityEngine;

[Tooltip("For normal platforms")]
public class MovingPlatform : MonoBehaviour
{
    [Header("Platform Settings")] 
    [SerializeField] private Transform[] points;
    [SerializeField] private int startingIndex;
    
    [SerializeField] private float speed;

    // caching
    private Vector3 _direction;
    private bool _hasChild;
    private int _i;
    private float _angle;
    
    void OnEnable()
    {
        transform.position = points[startingIndex].position;
        StartCoroutine(Move());
    }

    /// <summary>
    /// For normal Platforms
    /// </summary>
    /// <returns></returns>
    IEnumerator Move()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, points[_i].position) <= 0.02f)
            {
                _i += 1;
                if (_i == points.Length) _i = 0;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, points[_i].position, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (transform.position.y < other.transform.position.y || transform.position.y > other.transform.position.y)
            other.transform.SetParent(gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
