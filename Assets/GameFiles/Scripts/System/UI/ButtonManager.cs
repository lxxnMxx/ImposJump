using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : Singleton<ButtonManager>
{
    private int _nextTutorialIndex;
    private string _previousTutorialIndex;
    
    public void Reset()
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        UIManager.Instance.DeactivateGameOverPanel();
        UIManager.Instance.DeactivateFinishPanel();
        GameManager.Instance.ChangeGameState(GameState.StartGame);
    }

    public void Resume()
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        UIManager.Instance.DeactivatePausePanel();
        GameManager.Instance.ChangeGameState(GameManager.Instance.lastGameState);
    }

    public void SelectLevel(int index)
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        MenuManager.Instance.SelectLevel(index);
    }

    public void Play()
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        SceneHandler.Instance.LoadLevel(MenuManager.Instance.GetSelectedLevel());
    }

    public void MainMenu()
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        SceneHandler.Instance.LoadMainMenu();
    }

    public void NextTutorial()
    {
        _nextTutorialIndex = SceneHandler.Instance.tutorials.FindIndex(i => i == SceneManager.GetActiveScene().name);
        if(_nextTutorialIndex <= 3) // prevent the index from being out of bounds
            SceneHandler.Instance.LoadTutorial(SceneHandler.Instance.tutorials[_nextTutorialIndex+1]);
        else // load main menu if the index gets out of bounds
            SceneHandler.Instance.LoadMainMenu();
    }

    public void PreviousTutorial()
    {
        _nextTutorialIndex = SceneHandler.Instance.tutorials.FindIndex(i => i == SceneManager.GetActiveScene().name);
        if(_nextTutorialIndex > 1) // prevent the index from being out of bounds
            SceneHandler.Instance.LoadTutorial(SceneHandler.Instance.tutorials[_nextTutorialIndex-1]);
        else // load main menu if the index gets out of bounds
            SceneHandler.Instance.LoadMainMenu();
    }

    public void Quit()
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        Application.Quit();
    }

    public void PlayTutorial(string sceneName)
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        SceneHandler.Instance.LoadTutorial(sceneName);
    }

    public void OpenSettings()
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        UIElementHandler.Instance.GetCanvas("#Settings").gameObject.SetActive(true);
        UIElementHandler.Instance.GetCanvas("#MainMenu").gameObject.SetActive(false);
    }
    
    public void OpenTutorial()
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        UIElementHandler.Instance.GetCanvas("#TutorialSelection").gameObject.SetActive(true);
        UIElementHandler.Instance.GetCanvas("#MainMenu").gameObject.SetActive(false);
    }

    public void LeaveSettings()
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        UIElementHandler.Instance.GetCanvas("#MainMenu").gameObject.SetActive(true);
        UIElementHandler.Instance.GetCanvas("#Settings").gameObject.SetActive(false);
    }
    
    public void LeaveTutorial()
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        UIElementHandler.Instance.GetCanvas("#MainMenu").gameObject.SetActive(true);
        UIElementHandler.Instance.GetCanvas("#TutorialSelection").gameObject.SetActive(false);
    }
}
