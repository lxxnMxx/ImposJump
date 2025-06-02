using UnityEngine.SceneManagement;

public class ViewManager
{
    public static void LoadLevel(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        // check if there's an active Scene, if yes, unload it, if not, then not
        var scene = SceneHandler.Instance.levels[SceneHandler.Instance.levels.FindIndex(x => x == sceneName)];
        if (scene == sceneName)
        {
            SceneManager.UnloadSceneAsync(scene);
        }
        GameManager.Instance.ChangeGameState(GameState.StartGame);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        GameManager.Instance.ChangeGameState(GameState.MainMenu);
    }
}
