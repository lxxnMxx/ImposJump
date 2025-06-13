using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LevelManager : Singleton<LevelManager>, IDataPersistence
{
    public List<Level> levels;

    public void LoadData(GameData data)
    {
        levels = data.levels;
    }

    public void SaveData(ref GameData data)
    {
        levels.ForEach(level => level.isActive = false); // loop through levels and set isActive in ever level false
        data.levels = levels;
    }

    // just returns the index of the level that is activated
    public int GetActiveLevel() => levels.FindIndex(lvl => lvl.isActive);
}
