using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : DontDestroyOnLoad<ButtonManager>
{
    public void Reset()
    {
        GameManager.Instance.ChangeGameState(GameState.StartGame);
        ViewManager.LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void Resume()
    {
        GameManager.Instance.ChangeGameState(GameState.StartGame);
    }

    public void SelectLevel1()
    {
        MenuManager.Instance.SelectLevel(1);
    }
    public void SelectLevel2()
    {
        MenuManager.Instance.SelectLevel(2);
    }

    public void Play()
    {
        print(MenuManager.Instance.GetSelectedLevel());
        ViewManager.LoadLevel(MenuManager.Instance.GetSelectedLevel());
    }

    public void MainMenu()
    {
        ViewManager.UnloadLevel(SceneManager.GetActiveScene().name);
        ViewManager.LoadMainMenu();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
