using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [field:  SerializeField] // expose to the Unity Inspector
    public int PlayerDeaths {set; get;} //TODO: find a better solution to store, set and access these type of Data
    
    public GameState gameState;
    public GameState lastGameState;
    public int tutorialIndex;
    public event Action<GameState> OnGameStateChange;
    
    public event Action OnGameOver;
    public event Action OnGameStart;
    public event Action OnLevelFinished;


    private GameObject _player;
    
    
    private void OnEnable()
    {
        OnGameStateChange += ResumeGame;
        SceneHandler.Instance.OnSceneLoaded += OnSceneLoaded;
        OnLevelFinished += LevelFinished;
        OnGameStart += GameStarted;
    }

    private void OnDisable()
    {
        OnGameStateChange -= ResumeGame;
        OnLevelFinished -= LevelFinished;
        SceneHandler.Instance.OnSceneLoaded -= OnSceneLoaded;
        OnGameStart -= GameStarted;
    }

    private void OnSceneLoaded(string sceneName)
    {
        if(sceneName == "MainMenu") ChangeGameState(GameState.MainMenu);
        
        //print(SceneHandler.Instance.IsCurrentSceneTutorial() || SceneHandler.Instance.IsCurrentSceneLevel());
        
        if (SceneHandler.Instance.IsSceneTutorial(sceneName) || SceneHandler.Instance.IsSceneLevel(sceneName))
        {
            _player = GameObject.FindWithTag("Player");
        }
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && gameState is GameState.PauseMenu) ButtonManager.Instance.Resume();
        
        else if(Input.GetKeyDown(KeyCode.Escape) && gameState is GameState.GameContinues or GameState.Danger)
            ChangeGameState(GameState.PauseMenu);
        
        else if (Input.GetKeyDown(KeyCode.Return) && gameState is GameState.GameContinues or GameState.Danger)
        {
            if(!SceneHandler.Instance.IsCurrentSceneTutorial())
                LevelManager.Instance.levels[LevelManager.Instance.GetActiveLevel()].deathCount += 1;
            ButtonManager.Instance.Reset();
        }
        
        else if (Input.GetKeyDown(KeyCode.Return) && gameState is GameState.GameOver) ButtonManager.Instance.Reset();
    }

    public void ChangeGameState(GameState state)
    {
        lastGameState = gameState;
        gameState = state;
        print(gameState);
        OnGameStateChange?.Invoke(gameState);
        
        switch (state)
        {
            case GameState.MainMenu:
                UnlockCursor();
                break;
            
            case GameState.StartGame:
                OnGameStart?.Invoke();
                break;
            
            case GameState.GameContinues:
                LockCursor();
                break;
            
            case GameState.Danger:
                LockCursor();
                break;
            
            case GameState.PauseMenu:
                GamePaused();
                UnlockCursor();
                break;
            
            case GameState.GameOver:
                // set deathCount for each level
                if(!SceneHandler.Instance.IsCurrentSceneTutorial())
                    LevelManager.Instance.levels[LevelManager.Instance.GetActiveLevel()].deathCount += 1;
                UnlockCursor();
                OnGameOver?.Invoke();
                break;
            
            case GameState.LevelFinished:
                OnLevelFinished?.Invoke();
                UnlockCursor();
                break;
        }
    }

    private void GameStarted()
    {
        LockCursor();
        _player.SetActive(true);
        ChangeGameState(GameState.GameContinues);
    }

    private void GamePaused()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame(GameState state)
    {
        if(state == GameState.PauseMenu) return;
        Time.timeScale = 1;
    }

    private void LockCursor()
    {
        // Cursor Gets also locked in PlayerBase OnEnable()
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        // Unlock Cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LevelFinished()
    {
        SoundManager.Instance.Play(SoundType.LevelFinished);
    }
}
