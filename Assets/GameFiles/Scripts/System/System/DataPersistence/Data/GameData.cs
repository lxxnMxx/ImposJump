using System;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int deathCount; // TODO switch out deathcount with the deathcount of each level
    public List<string> levelsUnlocked;

    // values in constructor are all default values (when game has nothing to load)
    public GameData()
    {
        deathCount = 0;
        levelsUnlocked = new();
    }
}
