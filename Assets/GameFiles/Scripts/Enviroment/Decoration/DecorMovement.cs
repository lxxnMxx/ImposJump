using System.Collections;
using UnityEngine;

public class DecorMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;

    private void Start()
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