using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public record Level
{
    public string name;
    public bool isUnlocked;
    public bool isActive;
    public int deathCount;

    public Level(string name, bool isUnlocked, int deathCount)
    {
        this.name = name;
        this.isUnlocked = isUnlocked;
        this.deathCount = deathCount;
    }
}
