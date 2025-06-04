using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private UIElementHandler _elementHandler;
    
    private GameObject _gameOverPanel;
    private GameObject _pausePanel;
    private GameObject _finishPanel;
    
    private Text _deathCountTxt;

    private Slider _timeLeftBadCloud;

    
    
    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += GameOver;
        GameManager.Instance.OnGameStart += GameStart;
        GameManager.Instance.OnGameStateChange += PauseMenu;
        GameManager.Instance.OnGameStateChange += ResumeGame;
        GameManager.Instance.OnGameStateChange += Danger;
        GameManager.Instance.OnLevelFinished += LevelFinished;
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= GameOver;
        GameManager.Instance.OnGameStart -= GameStart;
        GameManager.Instance.OnGameStateChange -= PauseMenu;
        GameManager.Instance.OnGameStateChange -= ResumeGame;
        GameManager.Instance.OnGameStateChange -= Danger;
        GameManager.Instance.OnLevelFinished -= LevelFinished;
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Initialization of UI Elements
    // TODO: find a better solution for this DEFINITELY!!!! (imagine handling 5 levels with this)
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _elementHandler = UIElementHandler.Instance;
        if (scene.name == "LevelUI")
        {
            _deathCountTxt = _elementHandler.GetText("#DeathCount");
            _deathCountTxt.text = $"Deaths: {GameManager.Instance.PlayerDeaths}";
            
            _timeLeftBadCloud = _elementHandler.GetSlider("#TimeLeftBadCloud");
            
            _gameOverPanel = _elementHandler.GetPanel("#GameOverPanel");
            _pausePanel = _elementHandler.GetPanel("#PausePanel");
        }
    }

    public void SetTimeLeftBadCloudMaxValue(float maxValue)
    {
        _timeLeftBadCloud.maxValue = maxValue;
    }
    
    public void SetTimeLeftBadCloud(float timeLeft)
    {
        _timeLeftBadCloud.value = timeLeft;
    }
    
    private void PauseMenu(GameState state)
    {
        if (state != GameState.PauseMenu || !_pausePanel) return;
        _pausePanel.SetActive(true);
        
        // show the bar here that the player can see how much time he got left on this platform
        if (GameManager.Instance.lastGameState == GameState.Danger)
            _timeLeftBadCloud.gameObject.SetActive(true);
    }

    // if the player is in Danger
    private void Danger(GameState state)
    {
        // if the last GameState was pauseMenu then let the bar still active
        if (state == GameState.Danger)
        {
            _timeLeftBadCloud.gameObject.SetActive(true); return;
        }
        
        // return here cause when not, it would get deactivated in the pauseMenu
        if(GameManager.Instance.lastGameState == GameState.Danger && state == GameState.PauseMenu || !_timeLeftBadCloud) 
            return;
        _timeLeftBadCloud.gameObject.SetActive(false);
    }

    private void ResumeGame(GameState state)
    {
        if (state is GameState.PauseMenu or GameState.MainMenu or GameState.StartGame || !_pausePanel) return;
        _pausePanel?.SetActive(false);
    }

    private void GameOver()
    {
        _gameOverPanel.SetActive(true);
        _deathCountTxt.text = $"Deaths: {GameManager.Instance.PlayerDeaths}";
    }

    private void GameStart()
    {
        if (!_gameOverPanel || !_pausePanel) return;
        _gameOverPanel.SetActive(false);
        _pausePanel.SetActive(false);
    }

    private void LevelFinished()
    {
        _finishPanel = _elementHandler.GetPanel("#FinishPanel"); // Initialized here cause earlier not needed
        _finishPanel.SetActive(true);
    }
}
