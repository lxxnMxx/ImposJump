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
        GameManager.Instance.OnGameStart += GameStarted;
        GameManager.Instance.OnGameStateChange += MainMenu;
        
        if (IsSceneLevel())
            GameManager.Instance.ChangeGameState(GameState.StartGame);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= GameStarted;
        GameManager.Instance.OnGameStateChange -= MainMenu;
    }

    private void MainMenu(GameState state)
    {
        if (state is GameState.MainMenu)
            SceneManager.UnloadSceneAsync("LevelUI");
    }

    private void GameStarted()
    {
        if (SceneManager.sceneCount < 2)
        {
            SceneManager.LoadSceneAsync("LevelUI", LoadSceneMode.Additive);
        }
        
    }
    
    // if this check is true, then the current scene is a level
    public bool IsSceneLevel() => SceneManager.GetActiveScene().name ==
                                  levels[levels.FindIndex(x => x == SceneManager.GetActiveScene().name)];
    
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        GameManager.Instance.ChangeGameState(GameState.StartGame);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        GameManager.Instance.ChangeGameState(GameState.MainMenu);
    }
}
