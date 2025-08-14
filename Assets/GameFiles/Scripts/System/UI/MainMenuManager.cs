using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : Singleton<MainMenuManager>
{
    public int selectedLvlIndex = 0;

    [SerializeField] private string[] backgrounds;

    private void Start()
    {
        SelectLevel(2);
    }
    
    public void SelectLevel(int levelIndex)
    {
        selectedLvlIndex = levelIndex;
        ChangeBackground(levelIndex);   
    }

    public string GetSelectedLevel() => $"Level{selectedLvlIndex-1}";
    
    private void ChangeBackground(int levelIndex)
    {
        // ensure that only one additive Scene is loaded; Unload a Scene if it's active
        if (SceneManager.sceneCount > 1) 
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        // Load the background parallel to the actual Menu
        SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Additive);
    }
}
