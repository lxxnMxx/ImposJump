using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time;

    private Text _text;
    private double _seconds;

    private bool _isGameOver;

    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += StopCounting;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= StopCounting;
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
            _text.text = $"{_seconds:f2}";
        }
    }
    
    private void StopCounting() => _isGameOver = true;
}
