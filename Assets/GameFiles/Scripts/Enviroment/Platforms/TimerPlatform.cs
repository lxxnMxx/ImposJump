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

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += GameStarted;
        GameManager.Instance.OnGameOver += ResetTimer;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= GameStarted;
        GameManager.Instance.OnGameOver -= ResetTimer;
    }

    private void Start()
    {
        _timer = startTime;
    }

    private void Update()
    {
        if (!_player || !(_timer > 0)) return;
        _timer -= Time.deltaTime;
        UIManager.Instance.SetTimeLeftBadCloud(_timer);
        if (_timer <= 0)
        {
            GameManager.Instance.ChangeGameState(GameState.GameOver);
        }
    }

    private void GameStarted()
    {
        ResetTimer();
        _isFirstCollision = true;
    }

    private void ResetTimer()
    { _timer = startTime; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _isFirstCollision)
        {
            UIManager.Instance.SetTimeLeftBadCloudMaxValue(startTime);
            _isFirstCollision = false;
        }

        if (!collision.gameObject.CompareTag("Player")) return;
        _player = collision.gameObject;
        GameManager.Instance.ChangeGameState(GameState.Danger);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        _player = null;
        if(GameManager.Instance.GameState != GameState.GameOver)
            GameManager.Instance.ChangeGameState(GameState.GameContinues);
    }
}
