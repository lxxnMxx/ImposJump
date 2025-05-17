using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time;

    private Text _text;
    private double _seconds;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        time += Time.deltaTime;
        _seconds = Math.Round(time % 60, 2);
        _text.text = $"{_seconds:f2}";   
    }
}
