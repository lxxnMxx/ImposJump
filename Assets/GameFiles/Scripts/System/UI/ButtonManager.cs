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

    public void LoadLevel()
    {
        ViewManager.LoadLevel("Level1");
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
