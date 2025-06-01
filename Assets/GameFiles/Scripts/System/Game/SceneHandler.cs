using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private List<string> levels;
    
    private int _sceneIndex;
    private string _sceneName;
    
    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += GameStarted;
        GameManager.Instance.OnGameStateChange += MainMenu;
        
        if (SceneManager.GetActiveScene().name ==
            levels[levels.FindIndex(x => x == SceneManager.GetActiveScene().name)])
            GameManager.Instance.ChangeGameState(GameState.StartGame);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= GameStarted;
        GameManager.Instance.OnGameStateChange += MainMenu;
    }

    private void MainMenu(GameState state)
    {
        if (state is GameState.MainMenu)
            SceneManager.UnloadSceneAsync("LevelUI");
    }

    private void GameStarted()
    {
        print("Scenemanager does also exists!!!");
        if (SceneManager.GetActiveScene().name == "LevelUI")
        {
            return;
        }
            
        SceneManager.LoadSceneAsync("LevelUI", LoadSceneMode.Additive);
    }
}
