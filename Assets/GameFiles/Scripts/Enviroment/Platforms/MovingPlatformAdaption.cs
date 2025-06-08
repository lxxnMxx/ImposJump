using System;
using System.Collections;
using UnityEngine;

public class MovingPlatformAdaption : MonoBehaviour
{
    [Header("Platform Settings")] 
    [SerializeField] Transform[] points;
    [SerializeField] int startingIndex;
    
    [SerializeField] float speed;

    private Vector3 _direction;
    private bool _hasChild;
    private int _i;
    private float _angle;
    
    void OnEnable()
    {
        transform.position = points[startingIndex].position;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, points[_i].position) <= 0.02f)
            {
                _i += 1;
                if (_i == points.Length) _i = 0;
                
                // turn the platform to the current point
                _direction = transform.position - points[_i].position;
                _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(_angle, Vector3.up);
            }
            
            try
            {
                if (transform.GetChild(0)) 
                    transform.GetChild(0).transform.rotation = Quaternion.EulerAngles(0,0,0);
                
            }
            catch
            {
                print("");
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
