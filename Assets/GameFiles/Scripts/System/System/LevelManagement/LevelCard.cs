using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCard : MonoBehaviour 
{
    [SerializeField] private string levelName;
    
    [Header("Coin")]
    [SerializeField] private List<Image> coins;
    [SerializeField] private Sprite fullCoinSprite;
    [SerializeField] private Sprite emptyCoinSprite;
    
    [Header("Best time")]
    [SerializeField] private Text bestTimeText;

    private int _coinsToShowCount;
    private (int, double) bestTime;

    
    private void OnEnable()
    {
        _coinsToShowCount = LevelManager.Instance.GetCoinsForLevel(levelName);
        var hasCoins = _coinsToShowCount > 0;
        var i = 0;
        foreach (var coin in coins)
        {
            coin.sprite = hasCoins && i < _coinsToShowCount ? fullCoinSprite : emptyCoinSprite;
            i++;
        }

        bestTime = TimeConversioner.GetConvertedTime(LevelManager.Instance.GetLevel(levelName).bestTime);
        bestTimeText.text = $"{bestTime.Item1}:{bestTime.Item2:f2}";
    }
}
