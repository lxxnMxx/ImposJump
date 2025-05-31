using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private GameObject _gameOverPanel;
    private GameObject _pausePanel;
    
    private Text _deathCountTxt;

    private Slider _timeLeftBadCloud;

    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += GameOver;
        GameManager.Instance.OnGameStart += GameStart;
        GameManager.Instance.OnGameStateChange += PauseMenu;
        GameManager.Instance.OnGameStateChange += ResumeGame;
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= GameOver;
        GameManager.Instance.OnGameStart -= GameStart;
        GameManager.Instance.OnGameStateChange -= PauseMenu;
        GameManager.Instance.OnGameStateChange -= ResumeGame;
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
        {
            _deathCountTxt = UIElementHandler.Instance.GetText("#DeathCount");
            _deathCountTxt.text = $"Deaths: {GameManager.Instance.PlayerDeaths}";
            
            _gameOverPanel = UIElementHandler.Instance.GetPanel("#GameOverPanel");
            _pausePanel = UIElementHandler.Instance.GetPanel("#PausePanel");
            
            _timeLeftBadCloud = UIElementHandler.Instance.GetSlider("#TimeLeftBadCloud");
        
            // set Button events after SceneLoaded
            UIElementHandler.Instance.SetButtonEvent("#Restart", ButtonManager.Instance.Reset);
            UIElementHandler.Instance.SetButtonEvent("#Resume", ButtonManager.Instance.Resume);
            UIElementHandler.Instance.SetButtonEvent("#MainMenuGO", ButtonManager.Instance.MainMenu);
            UIElementHandler.Instance.SetButtonEvent("#MainMenuPause", ButtonManager.Instance.MainMenu);
        }

        if (scene.name == "MainMenu")
        {
            UIElementHandler.Instance.SetButtonEvent("#Level1", ButtonManager.Instance.SelectLevel1);
            UIElementHandler.Instance.SetButtonEvent("#Level2", ButtonManager.Instance.SelectLevel2);
            UIElementHandler.Instance.SetButtonEvent("#Play", ButtonManager.Instance.Play);
            UIElementHandler.Instance.SetButtonEvent("#Quit", ButtonManager.Instance.Quit);
            UIElementHandler.Instance.SetButtonEvent("#OpenSettings", ButtonManager.Instance.OpenSettings);
            UIElementHandler.Instance.SetButtonEvent("#BackSettings", ButtonManager.Instance.LeaveSettings);
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
        if (state != GameState.PauseMenu) return;
        _pausePanel.SetActive(true);
    }

    private void ResumeGame(GameState state)
    {
        if (state is GameState.PauseMenu) return;
        _pausePanel.SetActive(false);
    }

    private void GameOver()
    {
        _gameOverPanel.SetActive(true);
    }

    private void GameStart()
    {
        _gameOverPanel.SetActive(false);
    }
}
