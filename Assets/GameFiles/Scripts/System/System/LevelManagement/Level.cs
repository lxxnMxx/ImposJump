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
    public int deathCount;

    public Level(string name, SerializableDictionary<string, bool> collectedCoins, int deathCount)
    {
        this.name = name;
        coinsCollected = collectedCoins;
        this.deathCount = deathCount;
    }
}
