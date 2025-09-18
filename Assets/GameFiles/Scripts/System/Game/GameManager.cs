using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerData playerData;
    
    public GameState GameState { get; private set; }
    public GameState LastGameState { get; private set; }
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

    private void OnSceneLoaded(string sceneName)
    {
        if(sceneName == "MainMenu") 
            ChangeGameState(GameState.MainMenu);
        
        if(SceneHandler.Instance.IsSceneTutorial(sceneName) || SceneHandler.Instance.IsSceneLevel(sceneName))
        {
            _player = GameObject.FindWithTag("Player");
            _playerStartPosition = _player.transform.position;
        }
    }

	private void Start()
	{
		if(SceneManager.GetActiveScene().name == "LoadingScene") ChangeGameState(GameState.LoadingGame);
		if(SceneManager.GetActiveScene().name == "MainMenu") ChangeGameState(GameState.MainMenu);
	}

	void Update()
    {
        if(GameState is GameState.PauseMenu && Input.GetKeyDown(KeyCode.Return)) ButtonManager.Instance.Resume();

        else if(GameState is GameState.GameContinues or GameState.Danger && Input.GetKeyDown(KeyCode.Escape))
            ChangeGameState(GameState.PauseMenu);

        else if(GameState is GameState.GameContinues or GameState.Danger && Input.GetKeyDown(KeyCode.Return))
        {
            if(!SceneHandler.Instance.IsCurrentSceneTutorial())
                LevelManager.Instance.GetActiveLevel().deathCount += 1;
            ButtonManager.Instance.Reset();
        }

        else if(Input.GetKeyDown(KeyCode.Return) && GameState is GameState.GameOver) ButtonManager.Instance.Reset();
    }

    public void ChangeGameState(GameState state)
    {
        LastGameState = GameState;
        GameState = state;
        print(GameState);
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

            case GameState.PauseMenu:
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
        ChangeGameState(GameState.GameContinues);

        // I don't like the way how this is fixed ...
        if(_player == null) return; // question mark didn't work here (specifically at the transform.position access)
        _player.transform.position = _playerStartPosition;
        _player.SetActive(true);
        playerData.gravityDirection = 1;
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
        SoundManager.Instance.Play(SoundList.Player, SoundType.LevelFinished);
    }

    IEnumerator WaitForSceneLoaded(string sceneName)
    {
        yield return new WaitUntil(() => SceneManager.GetSceneByName(sceneName).isLoaded);
        SceneHandler.Instance.LoadMainMenu();
    }
}