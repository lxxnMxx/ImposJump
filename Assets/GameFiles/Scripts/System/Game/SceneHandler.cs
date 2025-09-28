using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Task = System.Threading.Tasks.Task;

public class SceneHandler : Singleton<SceneHandler>
{
    public List<string> tutorials;
    public event Action<string> OnSceneLoaded;
    
    private LevelManager _levelManager;
    private int _levelIndex;
    
    private int _sceneIndex;
    private string _sceneName;
    private AsyncOperation _scene;

    private void Start()
    {
        _levelManager = LevelManager.Instance;
    }

    // .Any just iterates through the collection, and if the condition is true, return true, otherwise false
    public bool IsCurrentSceneLevel() => _levelManager.levels.Any(lvl => lvl.name == SceneManager.GetActiveScene().name);
    public bool IsCurrentSceneTutorial() => tutorials.Any(tut => tut == SceneManager.GetActiveScene().name);
    
    public bool IsSceneLevel(string sceneName) => _levelManager.levels.Any(lvl => lvl.name == sceneName);
    public bool IsSceneTutorial(string sceneName) => tutorials.Any(tut => tut == sceneName);

    
    public async void LoadLevel(string sceneName)
    {
        _levelIndex =_levelManager.levels.FindIndex(lvl => lvl.name == sceneName);
        _levelManager.levels[_levelIndex].isActive = true;
        await LoadScene(sceneName, LoadSceneMode.Single);
        await LoadScene("LevelUI", LoadSceneMode.Additive);
    }
    
    public async void LoadTutorial(string sceneName)
    {
        await LoadScene(sceneName, LoadSceneMode.Single);
        await LoadScene("TutorialUI", LoadSceneMode.Additive);
    }

    public async void LoadMainMenuFromLevel()
    {
        if(!IsCurrentSceneTutorial())
            _levelManager.GetActiveLevel().isActive = false;
        LoadMainMenu();
	}

    public async Task<int> LoadScene(string sceneName, LoadSceneMode mode)
    {
        _scene = SceneManager.LoadSceneAsync(sceneName, mode);
        if(_scene == null)
        {
            Debug.LogError($"Scene '{sceneName}' could not be loaded.");
			return 0;
        }
        while (_scene.progress < 1f) // wait til scene's loaded
        {
            await Task.Yield();
        }
		OnSceneLoaded?.Invoke(sceneName);
        return 0;
    }

	public async void LoadMainMenu()
	{
		await LoadScene("MainMenu", LoadSceneMode.Single);
	}
}
