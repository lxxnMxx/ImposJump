using System.Collections;
using UnityEngine;

public abstract class MovingPlatformBase : MonoBehaviour
{
    [Header("Platform Settings")] 
    [SerializeField] protected Transform[] points;
    [SerializeField] protected int startingIndex;
    
    [SerializeField] protected float speed;
    
    // caching
    protected Vector3 Direction;
    protected int I;
    protected float Angle;
    
    
    void OnEnable()
    {
        transform.position = points[startingIndex].position;
        StartCoroutine(Move());
    }
    
    protected abstract IEnumerator Move();
    
    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (transform.position.y < other.transform.position.y || transform.position.y > other.transform.position.y)
            other.transform.SetParent(gameObject.transform);
    }

    protected void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
