using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TimerPlatform : MonoBehaviour
{
    [SerializeField] private float startTime;

    private bool _isFirstCollision = true;
    private float _timer;
    private GameObject _player;

    private void Start()
    {
        _timer = startTime;
    }

    private void Update()
    {
        if (_player && _timer > 0)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                GameManager.Instance.ChangeGameState(GameState.GameOver);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _isFirstCollision)
        {
            _isFirstCollision = false;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = null;
        }
    }
}
