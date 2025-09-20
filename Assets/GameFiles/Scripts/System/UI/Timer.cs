using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    
    public float Time {get; private set;}

    private Text _text;
    private double _seconds;
    private int _minutes;

    private bool _isCounting;

    private void Awake()
    {
        Instance = this;
    }

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
        if (_isCounting) return;
        Time += UnityEngine.Time.deltaTime;
        _seconds = Math.Round(Time % 60, 2);
        _minutes = (int)Math.Round(Time / 120, 0);
        _text.text = $"{_minutes}:{_seconds:f2}"; // the f2 means that the text doesn't get tinier if the number is tinier
    }
    
    private void ResetTimer() 
    {
        _isCounting = false;
        Time = 0;
    }
    
    private void StopCounting() => _isCounting = true;
}
