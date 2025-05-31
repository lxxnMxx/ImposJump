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
            UIManager.Instance.SetTimeLeftBadCloud(_timer);
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
            UIManager.Instance.SetTimeLeftBadCloudMaxValue(startTime);
            _isFirstCollision = false;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = collision.gameObject;
            GameManager.Instance.ChangeGameState(GameState.Danger);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = null;
            if(GameManager.Instance.gameState != GameState.GameOver)
                GameManager.Instance.ChangeGameState(GameState.GameContinues);
        }
    }
}
