using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    public int selectedLvlIndex = 0;
    public Action OnSelectedLevelChanged;

    [SerializeField] private string[] backgrounds;

    private void Start()
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
        if (SceneManager.sceneCount == 1)
            SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Additive);
    }
}
