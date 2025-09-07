using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : Singleton<MainMenuManager>
{
    public string SelectedLevelName { get; private set; }

    [SerializeField] private string[] backgrounds;

    private void Start()
    {
        SelectLevel("Level1");
    }
    
    public void SelectLevel(string levelName)
    {
        SelectedLevelName = levelName;
        ChangeBackground(levelName);
    }
    
    private void ChangeBackground(string levelName)
    {
        // ensure that only one additive Scene is loaded; Unload a Scene if it's active
        if (SceneManager.sceneCount > 1) 
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        SceneManager.LoadSceneAsync($"{levelName}Bg", LoadSceneMode.Additive);
    }
}
