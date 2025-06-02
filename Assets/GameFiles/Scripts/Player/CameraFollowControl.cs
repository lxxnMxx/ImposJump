using System;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraFollowControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [Header("x is above ; y is under")]
    [SerializeField] private Vector2 yStopPoint;

    private Transform _player;

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += GameStarted;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= GameStarted;
    }

    private void Start()
    {
        _player = gameObject.transform;
    }

    private void Update()
    {
        //      player falling                           player raising
        if (_player.position.y < yStopPoint.y || _player.position.y > yStopPoint.x)
        {
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
            GameManager.Instance.ChangeGameState(GameState.GameOver);
        }
    }

    private void GameStarted()
    {
        virtualCamera.Follow = _player;
        virtualCamera.LookAt = _player;
    }
}
