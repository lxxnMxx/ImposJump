using System;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public List<Level> levels;
    public List<Skin> skins;

    // values in constructor are all default values (when game has nothing to load)
    public GameData()
    {
        skins = new List<Skin>();
        levels = new List<Level>(new []
        {
            new Level("Level1", new SerializableDictionary<string, bool>(),0),
            new Level("Level2", new SerializableDictionary<string, bool>(),0),
            new Level("Level3", new SerializableDictionary<string, bool>(),0),
            new Level("Level1Reloaded", new SerializableDictionary<string, bool>(),0),
            new Level("Level2Reloaded", new SerializableDictionary<string, bool>(),0)
        });
    }
}
