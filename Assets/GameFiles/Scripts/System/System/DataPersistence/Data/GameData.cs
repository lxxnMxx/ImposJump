using System;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public List<Level> levels;

    // values in constructor are all default values (when game has nothing to load)
    public GameData()
    {
        levels = new List<Level>(new []
        {
            new Level("Level1", true, 0),
            new Level("Level2", false, 0),
            new Level("Level2Reloaded", false, 0)
        });
    }
}
