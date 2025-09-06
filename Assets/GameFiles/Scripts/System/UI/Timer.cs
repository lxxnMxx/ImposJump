using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    
    [SerializeField] private float time;

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
        if (!_isCounting)
        {
            time += Time.deltaTime;
            _seconds = Math.Round(time % 60, 2);
            _minutes = (int)Math.Round(time / 120, 0);
            _text.text = $"{_minutes}:{_seconds:f2}"; // the f2 means that the text doesn't get tinier if the number is tinier
        }
    }

    public float GetTime() => time;

    private void ResetTimer() 
    {
        _isCounting = false;
        time = 0;
    }
    
    private void StopCounting() => _isCounting = true;
}
