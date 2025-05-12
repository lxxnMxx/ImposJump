using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewManager
{
    public static void LoadLevel(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        GameManager.Instance.ChangeGameState(GameState.StartGame);
    }

    public static void UnloadLevel(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        GameManager.Instance.ChangeGameState(GameState.MainMenu);
    }
}
