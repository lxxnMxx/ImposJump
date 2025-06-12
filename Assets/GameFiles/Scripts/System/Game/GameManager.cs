using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>, IDataPersistence
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
    private Vector3 _playerStartPosition;
    
    
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
    
    public void LoadData(GameData data)
    {
        PlayerDeaths = data.deathCount;
    }

    public void SaveData(ref GameData data)
    {
        data.deathCount = PlayerDeaths;
    }

    private void OnSceneLoaded(string sceneName)
    {
        if(sceneName == "MainMenu") ChangeGameState(GameState.MainMenu);
        
        //print(SceneHandler.Instance.IsCurrentSceneTutorial() || SceneHandler.Instance.IsCurrentSceneLevel());
        
        if (SceneHandler.Instance.IsSceneTutorial(sceneName) || SceneHandler.Instance.IsSceneLevel(sceneName))
        {
            _player = GameObject.FindWithTag("Player");
            _playerStartPosition = _player.transform.position;
        }
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && gameState is GameState.PauseMenu) ButtonManager.Instance.Resume();
        
        else if(Input.GetKeyDown(KeyCode.Escape) && gameState is GameState.GameContinues or GameState.Danger)
            ChangeGameState(GameState.PauseMenu);
        
        else if (Input.GetKeyDown(KeyCode.Return) && gameState is GameState.GameContinues or GameState.Danger)
        {
            PlayerDeaths += 1;
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
                PlayerDeaths += 1;
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
        ChangeGameState(GameState.GameContinues);
        
        // I don't like the way how this is fixed ...
        if(_player == null) return; // question mark didn't work here (specifically at the transform.position access)
        _player.transform.position = _playerStartPosition;
        _player.SetActive(true);
        _player.GetComponent<PlayerCollider>().gravityDirection = 1;
        _player.GetComponent<Rigidbody2D>().gravityScale = 2.7f;
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
