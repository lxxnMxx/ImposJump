using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [field:  SerializeField] // expose to the Unity Inspector
    public int PlayerDeaths {set; get;} //TODO: find a better solution to store, set and access these type of Data
    
    public GameState gameState;
    public event Action<GameState> OnGameStateChange;
    
    public event Action OnGameOver;
    public event Action OnGameStart;

    private void OnEnable()
    {
        OnGameStateChange += ResumeGame;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        OnGameStateChange -= ResumeGame;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainMenu") ChangeGameState(GameState.MainMenu);
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && gameState is GameState.PauseMenu) ButtonManager.instance.Resume();
        
        else if(Input.GetKeyDown(KeyCode.Escape) && gameState is GameState.StartGame) ChangeGameState(GameState.PauseMenu);
        else if (Input.GetKeyDown(KeyCode.Return) && gameState is GameState.StartGame)
        {
            PlayerDeaths += 1;
            ButtonManager.instance.Reset();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && gameState is GameState.GameOver) ButtonManager.instance.Reset();
    }

    public void ChangeGameState(GameState state)
    {
        gameState = state;
        //print(gameState);
        OnGameStateChange?.Invoke(gameState);
        
        switch (state)
        {
            case GameState.MainMenu:
                break;
            case GameState.StartGame:
                OnGameStart?.Invoke();
                break;
            case GameState.PauseMenu:
                GamePaused();
                break;
            case GameState.GameOver:
                PlayerDeaths += 1;
                OnGameOver?.Invoke();
                break;
        }
    }

    private void GamePaused()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame(GameState state)
    {
        if(state == GameState.PauseMenu)  return;
        Time.timeScale = 1;
    }
}
