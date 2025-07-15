using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class LevelManager : Singleton<LevelManager>, IDataPersistence
{
    public List<Level> levels;
    
    private int _coins;
    private int _coinsPerLevel;

    public void LoadData(GameData data)
    {
        levels = data.levels;
    }

    public void SaveData(ref GameData data)
    {
        levels.ForEach(level => level.isActive = false); // loop through levels and set isActive in every level false
        data.levels = levels;
    }

    // just returns the index of the level that is activated
    public int GetActiveLevel() => levels.FindIndex(lvl => lvl.isActive);

    public int GetAllCoins()
    {
        _coins = 0;
        foreach (var lvl in levels)
        {
            foreach (var coin in lvl.coinsCollected)
            {
                if(coin.Value) _coins += 1;
            }
        }
        return _coins;
    }

    public int GetCoinsForLevel(string levelName)
    {
        _coinsPerLevel = 0;
        foreach (var lvl in levels)
        {
            if (lvl.name == levelName)
            {
                foreach(var coin in lvl.coinsCollected)
                    if(coin.Value) _coinsPerLevel++;
            }
        }
        return _coinsPerLevel;
    }
}
