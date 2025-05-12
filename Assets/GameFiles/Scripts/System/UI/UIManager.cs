using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private GameObject _gameOverPanel;
    private GameObject _pausePanel;
    
    private Text _deathCountTxt;

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

    private async void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
        {
            _deathCountTxt = UIElementHandler.Instance.GetText("#DeathCount");
            _deathCountTxt.text = $"Deaths: {GameManager.Instance.PlayerDeaths}";
            
            _gameOverPanel = UIElementHandler.Instance.GetPanel("#GameOverPanel");
            _pausePanel = UIElementHandler.Instance.GetPanel("#PausePanel");
        
            // set Button events after SceneLoaded
            UIElementHandler.Instance.SetButtonEvent("#Restart", ButtonManager.instance.Reset);
            UIElementHandler.Instance.SetButtonEvent("#Resume", ButtonManager.instance.Resume);
            UIElementHandler.Instance.SetButtonEvent("#MainMenuGO", ButtonManager.instance.MainMenu);
            UIElementHandler.Instance.SetButtonEvent("#MainMenuPause", ButtonManager.instance.MainMenu);
        }

        if (scene.name == "MainMenu")
        {
            UIElementHandler.Instance.SetButtonEvent("#Level1", ButtonManager.instance.LoadLevel);
            UIElementHandler.Instance.SetButtonEvent("#Quit", ButtonManager.instance.Quit);
        }
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
