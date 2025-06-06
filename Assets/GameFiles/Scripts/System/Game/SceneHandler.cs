using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    public List<string> levels;
    public List<string> tutorials;
    
    private int _sceneIndex;
    private string _sceneName;
    
    // if this check is true, then the current scene is a level
    // .Any just iterates through the collection, and if the condition is true, return true, otherwise false
    public bool IsCurrentSceneLevel() => levels.Any(lvl => lvl == SceneManager.GetActiveScene().name);
    public bool IsCurrentSceneTutorial() => tutorials.Any(tut => tut == SceneManager.GetActiveScene().name);

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        SceneManager.LoadSceneAsync("LevelUI", LoadSceneMode.Additive);
        GameManager.Instance.ChangeGameState(GameState.StartGame);
    }
    
    public void LoadTutorial(int index)
    {
        SceneManager.LoadSceneAsync($"Tutorial{index}", LoadSceneMode.Single);
        SceneManager.LoadSceneAsync("LevelUI", LoadSceneMode.Additive);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        GameManager.Instance.ChangeGameState(GameState.MainMenu);
    }
}
