using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    public List<string> levels;
    
    private int _sceneIndex;
    private string _sceneName;
    
    private void OnEnable()
    {
        //GameManager.Instance.OnGameStart += GameStarted;
        
        if (IsSceneLevel() && GameManager.Instance.lastGameState == null)
            GameManager.Instance.ChangeGameState(GameState.StartGame);
    }

    private void OnDisable()
    {
        //GameManager.Instance.OnGameStart -= GameStarted;
    }

    private void GameStarted()
    {
        
    }
    
    // if this check is true, then the current scene is a level
    // .Any just iterates through the collection, and if the condition is true, return true, otherwise false
    public bool IsSceneLevel() => levels.Any(lvl => lvl == SceneManager.GetActiveScene().name);

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        SceneManager.LoadSceneAsync("LevelUI", LoadSceneMode.Additive);
        GameManager.Instance.ChangeGameState(GameState.StartGame);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        GameManager.Instance.ChangeGameState(GameState.MainMenu);
    }
}
