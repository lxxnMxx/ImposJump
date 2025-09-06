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

    private int _coinsToShowCount;

    
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
    }
}
