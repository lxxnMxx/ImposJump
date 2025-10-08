using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>, IDataPersistence
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameManagerDataScriptableObject gameManagerDataScriptableObject;
    
    public GameState GameState { get; private set; }
    public GameState LastGameState { get; private set; }
    public event Action<GameState> OnGameStateChange;

    public event Action OnGameOver;
    public event Action OnGameStart;
    public event Action OnLevelFinished;

    public bool IsPlaying => GameState is GameState.Danger or GameState.GameContinues or GameState.StartGame;


    private GameObject _player;
    private Vector3 _playerStartPosition;
    private InputManager _inputManager;

    private new void Awake()
    {
        gameManagerDataScriptableObject.gameManagerData.isFirstStart = false;
    }
    
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
        if(sceneName == "MainMenu") 
            ChangeGameState(GameState.MainMenu);

        if (!SceneHandler.Instance.IsSceneTutorial(sceneName) && !SceneHandler.Instance.IsSceneLevel(sceneName)) return;
        _player = GameObject.FindWithTag("Player");
        _playerStartPosition = _player.transform.position;
    }

	private void Start()
	{
		if(SceneManager.GetActiveScene().name == "LoadingScene") 
            ChangeGameState(GameState.LoadingGame);
		if(SceneManager.GetActiveScene().name == "MainMenu") 
            ChangeGameState(GameState.MainMenu);
	}
    
    public void LoadData(GameData data)
    {
        gameManagerDataScriptableObject.gameManagerData = data.gameManagerData;
    }

    public void SaveData(ref GameData data)
    {
        data.gameManagerData = gameManagerDataScriptableObject.gameManagerData;
    }

    public void ChangeGameState(GameState state)
    {
        LastGameState = GameState;
        GameState = state;
        OnGameStateChange?.Invoke(GameState);

        switch(state)
        {
            case GameState.LoadingGame:
                StartCoroutine(WaitForSceneLoaded("LoadingScene"));
                break;

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

            case GameState.Pause:
                GamePaused();
                UnlockCursor();
                break;

            case GameState.GameOver:
                // set deathCount for each level
                if(!SceneHandler.Instance.IsCurrentSceneTutorial())
                    LevelManager.Instance.GetActiveLevel().deathCount += 1;
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

        if(_player == null) return;
        _player.transform.position = _playerStartPosition;
        _player.SetActive(true);
        
        ChangeGameState(GameState.GameContinues);
    }

    private void GamePaused()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame(GameState state)
    {
        if(state == GameState.Pause) return;
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
        SoundManager.Instance.Play(SoundList.Player, SoundType.LevelFinished);
    }

    IEnumerator WaitForSceneLoaded(string sceneName)
    {
        yield return new WaitUntil(() => SceneManager.GetSceneByName(sceneName).isLoaded);
        SceneHandler.Instance.LoadMainMenu();
    }
}
