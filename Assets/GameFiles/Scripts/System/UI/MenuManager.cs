using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    public int selectedLvlIndex = 0;

    [SerializeField] private string[] backgrounds;

    private void Awake()
    {
        SelectLevel(1);
    }
    
    public void SelectLevel(int levelIndex)
    {
        selectedLvlIndex = levelIndex;
        ChangeBackground(levelIndex);   
    }

    public string GetSelectedLevel() => $"Level{selectedLvlIndex}";

    private void ChangeBackground(int levelIndex)
    {
        // ensure that only one additive Scene is loaded; Unload a Scene if it's active
        if (SceneManager.sceneCount > 1) 
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        // Load the background parallel to the actual Menu
        SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Additive);
    }
}
