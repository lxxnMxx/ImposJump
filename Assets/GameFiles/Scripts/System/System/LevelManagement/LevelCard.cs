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

    private int _coinsToShowCount;

    private void Start()
    {
        _coinsToShowCount = LevelManager.Instance.GetCoinsForLevel(levelName);
        for (int i = 0; i < _coinsToShowCount; i++)
        {
            coins[i].sprite = fullCoinSprite;
        }
    }
}
