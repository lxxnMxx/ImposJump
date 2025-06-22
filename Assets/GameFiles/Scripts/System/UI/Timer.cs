using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time;

    private Text _text;
    private double _seconds;
    private int _minutes;

    private bool _isGameOver;

    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += StopCounting;
        GameManager.Instance.OnGameStart += ResetTimer;
        GameManager.Instance.OnLevelFinished += StopCounting;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= StopCounting;
        GameManager.Instance.OnLevelFinished -= StopCounting;
        GameManager.Instance.OnGameStart -= ResetTimer;
    }

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        if (!_isGameOver)
        {
            time += Time.deltaTime;
            _seconds = Math.Round(time % 60, 2);
            _minutes = (int)Math.Round(time / 120, 0);
            _text.text = $"{_minutes}:{_seconds:f2}"; // the f2 means that the text doesn't get tinier if the number is tinier
        }
    }

    private void ResetTimer() 
    {
        _isGameOver = false;
        time = 0;
    }
    
    private void StopCounting() => _isGameOver = true;
}
