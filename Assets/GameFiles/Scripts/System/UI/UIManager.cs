using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public event Action<string> OnCanvasLoad;
    
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
        
        SceneHandler.Instance.OnSceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= GameOver;
        GameManager.Instance.OnGameStart -= GameStart;
        GameManager.Instance.OnGameStateChange -= PauseMenu;
        GameManager.Instance.OnGameStateChange -= ResumeGame;
        GameManager.Instance.OnGameStateChange -= Danger;
        GameManager.Instance.OnLevelFinished -= LevelFinished;
        
        SceneHandler.Instance.OnSceneLoaded -= OnSceneLoaded;
    }

    // Initialization of UI Elements
    private void OnSceneLoaded(string sceneName)
    {
        _elementHandler = UIElementHandler.Instance;
        if (sceneName == "LevelUI" || sceneName == "TutorialUI")
        {
            GameManager.Instance.ChangeGameState(GameState.StartGame);
            _timeLeftBadCloud = _elementHandler.GetSlider("#TimeLeftBadCloud");
            
            _gameOverPanel = _elementHandler.GetPanel("#GameOverPanel");
            _pausePanel = _elementHandler.GetPanel("#PausePanel");
            _finishPanel = _elementHandler.GetPanel("#FinishPanel");
            _deathCountTxt = _elementHandler.GetText("#DeathCount");
        }
    }

    public void DeactivateFinishPanel() {if(_finishPanel) _finishPanel.SetActive(false);}
    public void DeactivateGameOverPanel() {if(_gameOverPanel) _gameOverPanel.SetActive(false);}
    public void DeactivatePausePanel() {if(_pausePanel) _pausePanel.SetActive(false);}
    
    public void SetTimeLeftBadCloudMaxValue(float maxValue)
    {
        _timeLeftBadCloud.maxValue = maxValue;
    }
    
    public void SetTimeLeftBadCloud(float timeLeft)
    {
        _timeLeftBadCloud.value = timeLeft;
    }

    public void CallOnCanvasLoaded(string cnvsName)
    {
        OnCanvasLoad?.Invoke(cnvsName);
    }

    #region EventFunctions

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
        _pausePanel.SetActive(false);
    }

    private void GameOver()
    {
        _gameOverPanel.SetActive(true);
        if (!_deathCountTxt || SceneHandler.Instance.IsCurrentSceneLevel())
            _deathCountTxt.text = $"Deaths: {LevelManager.Instance.levels[LevelManager.Instance.GetActiveLevel()].deathCount}";
    }

    private void GameStart()
    {
        if (!_deathCountTxt)
            _deathCountTxt = _elementHandler.GetText("#DeathCount");
        if (SceneHandler.Instance.IsCurrentSceneLevel())
            _deathCountTxt.text = $"Deaths: {LevelManager.Instance.levels[LevelManager.Instance.GetActiveLevel()].deathCount}";
    }

    private void LevelFinished()
    {
        _finishPanel.SetActive(true);
    }

    #endregion
    
}
