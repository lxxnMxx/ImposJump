using System;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraFollowControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [Range(-1,-40)]
    [SerializeField] private float yStopPoint;

    private Transform _player;

    private void Start()
    {
        _player = gameObject.transform;
    }

    void Update()
    {
        if (_player.position.y < yStopPoint)
        {
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
        }
    }
}
