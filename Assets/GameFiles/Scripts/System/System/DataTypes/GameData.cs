using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<Level> levels;
    public List<Skin> skins;
    public Settings settings;

    // values in constructor are all default values (when game has nothing to load)
    public GameData()
    {
        skins = new List<Skin>(new[]
        {
            new Skin("StandardSkin", 0, true, new Color(0.003921569f, 0.8078431f, 0.8509804f, 1), true),
            new Skin("GreenSkin", 3, false, new Color(0.009265613f,0.7987421f,0 , 1), false),
            new Skin("OrangeSkin", 6, false, new Color(0.898f, 0.459f, 0.000f, 1), false),
            new Skin("PurpleSkin", 9, false, new Color(0.898f, 0.000f, 0.710f, 1), false),
            new Skin("DarkBlueSkin", 12, false, new Color(0, 0.09019608f, 0.8980392f, 1), false),
            new Skin("RedSkin", 15, false, new Color(1, 0.1135371f, 0, 1), false)
        });
        
        levels = new List<Level>(new []
        {
            new Level("Level1", new SerializableDictionary<string, bool>(),0, 0f),
            new Level("Level2", new SerializableDictionary<string, bool>(),0, 0f),
            new Level("Level3", new SerializableDictionary<string, bool>(),0, 0f),
            new Level("Level4", new SerializableDictionary<string, bool>(),0, 0f),
            new Level("Level5", new SerializableDictionary<string, bool>(),0, 0f)
        });
        settings = new Settings();
	}
}
