using System;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;

    private Vector2 _startPos;
    private float _startZ;
    private Vector2 _newPos;

    private Vector2 Travel => (Vector2)cam.transform.position - _startPos;
    private float DistanceFromTarget => transform.position.z - target.position.z;
    private float ClippingPlane => cam.transform.position.z + (DistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane); 
    private float ParallaxFactor => Mathf.Abs(DistanceFromTarget) / ClippingPlane;
    
    private void Start()
    {
        _startPos = transform.position;
        _startZ = transform.position.z;
    }

    private void Update()
    {
        _newPos = _startPos + Travel * ParallaxFactor;
        transform.position = new Vector3(_newPos.x, _newPos.y, _startZ); // TODO: fix!!
    }
}
