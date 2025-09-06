using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public record Level
{
    public string name;
    public SerializableDictionary<string, bool> coinsCollected;
    public bool isActive;
    public float bestTime;
    public int deathCount;

    public Level(string name, SerializableDictionary<string, bool> collectedCoins, int deathCount, float bestTime)
    {
        this.name = name;
        coinsCollected = collectedCoins;
        this.deathCount = deathCount;
        this.bestTime = bestTime;
    }
}
