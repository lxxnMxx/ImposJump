using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : Singleton<ButtonManager>
{
    public void Reset()
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        GameManager.Instance.ChangeGameState(GameState.StartGame);
    }

    public void Resume()
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        GameManager.Instance.ChangeGameState(GameManager.Instance.lastGameState);
    }

    public void SelectLevel1()
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        MenuManager.Instance.SelectLevel(1);
    }
    public void SelectLevel2()
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        MenuManager.Instance.SelectLevel(2);
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

    public void Quit()
    {
        SoundManager.Instance.Play(SoundType.ButtonClick);
        Application.Quit();
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
