using System;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed;
    
    [SerializeField] private Vector3 offset;

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += GameStarted;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart += GameStarted;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    private void GameStarted()
    {
        transform.position = target.position;
    }
}
