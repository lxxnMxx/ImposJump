using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class LevelManager : Singleton<LevelManager>, IDataPersistence
{
    public List<Level> levels;
    
    private int _coins;
    private int _coinsPerLevel;

    public void LoadData(GameData data)
    {
        _coins = 0;
        _coinsPerLevel = 0;
        levels = data.levels;
    }

    public void SaveData(ref GameData data)
    {
        levels.ForEach(level => level.isActive = false);
        data.levels = levels;
    }
    
    /// <summary>
    /// Iterates through the levels list and returns the active level
    /// </summary>
    /// <returns>A Level Object</returns>
    public Level GetActiveLevel() => levels[levels.FindIndex(lvl => lvl.isActive)];
    
    /// <summary>
    /// Get any level
    /// </summary>
    /// <param name="lvlName">the name of the level to get</param>
    /// <returns></returns>
    public Level GetLevel(string lvlName) => levels.Find(lvl => lvl.name == lvlName);
    
    /// <summary>
    /// Counts every coin in every level that the player ever has collected.
    /// </summary>
    /// <returns>an integer of all collected coins</returns>
    public int GetAllCoins()
    {
        _coins = 0;
        foreach (var coin in 
                 from lvl in levels 
                 from coin in lvl.coinsCollected 
                 where coin.Value select coin)
        {
            _coins += 1;
        }
        return _coins;
    }

    /// <summary>
    /// Counts every collected coin in the given level and returns this count.
    /// </summary>
    /// <param name="levelName">the name of the level</param>
    /// <returns>an integer of all collected coins in the given level</returns>
    public int GetCoinsForLevel(string levelName)
    {
        _coinsPerLevel = 0;
        foreach (var coin in 
                 from lvl in levels 
                 where lvl.name == levelName 
                 from coin in lvl.coinsCollected 
                 where coin.Value select coin)
        {
            _coinsPerLevel++;
        }

        return _coinsPerLevel;
    }

    /// <summary>
    /// This method just sets the best time of the current level.
    /// </summary>
    public void SetBestTime()
    {
        var time = Timer.Instance.Time;
        var bestTime = GetActiveLevel().bestTime;
        if (bestTime == 0 || time <= bestTime) GetActiveLevel().bestTime = time;
    }
}
