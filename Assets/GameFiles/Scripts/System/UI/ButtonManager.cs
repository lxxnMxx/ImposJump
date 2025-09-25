using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonManager : Singleton<ButtonManager>
{
    [SerializeField] private PlayerData playerData;
    
    private int _nextTutorialIndex;
    
    public void Reset()
    {
        SoundManager.Instance.Play(SoundList.UI, SoundType.ButtonClick);
        UIManager.Instance.DeactivateGameOverPanel();
        UIManager.Instance.DeactivateFinishPanel();
        GameManager.Instance.ChangeGameState(GameState.StartGame);
    }

    public void Resume()
    {
        SoundManager.Instance.Play(SoundList.UI, SoundType.ButtonClick);
        UIManager.Instance.DeactivatePausePanel();
        GameManager.Instance.ChangeGameState(GameManager.Instance.LastGameState);
    }

    public void SelectLevel(string levelName)
    {
        SoundManager.Instance.Play(SoundList.UI, SoundType.ButtonClick);
        MainMenuManager.Instance.SelectLevel(levelName);
    }

    public void Play()
    {
        SoundManager.Instance.Play(SoundList.UI, SoundType.ButtonClick);
        SceneHandler.Instance.LoadLevel(MainMenuManager.Instance.SelectedLevelName);
    }

    public void MainMenu()
    {
        SoundManager.Instance.Play(SoundList.UI, SoundType.ButtonClick);
        SceneHandler.Instance.LoadMainMenuFromLevel();
    }

    public void NextTutorial()
    {
        _nextTutorialIndex = SceneHandler.Instance.tutorials.FindIndex(i => i == SceneManager.GetActiveScene().name);
        if(_nextTutorialIndex <= 3) // prevent the index from being out of bounds
            SceneHandler.Instance.LoadTutorial(SceneHandler.Instance.tutorials[_nextTutorialIndex+1]);
        else // load main menu if the index gets out of bounds
            SceneHandler.Instance.LoadMainMenuFromLevel();
    }

    public void PreviousTutorial()
    {
        _nextTutorialIndex = SceneHandler.Instance.tutorials.FindIndex(i => i == SceneManager.GetActiveScene().name);
        if(_nextTutorialIndex > 1)
            SceneHandler.Instance.LoadTutorial(SceneHandler.Instance.tutorials[_nextTutorialIndex-1]);
        else
            SceneHandler.Instance.LoadMainMenuFromLevel();
    }

    public void Quit()
    {
        SoundManager.Instance.Play(SoundList.UI, SoundType.ButtonClick);
        Application.Quit();
    }

    public void PlayTutorial(string sceneName)
    {
        SoundManager.Instance.Play(SoundList.UI, SoundType.ButtonClick);
        SceneHandler.Instance.LoadTutorial(sceneName);
    }

    public void OpenCanvas(string id)
    {
        SoundManager.Instance.Play(SoundList.UI, SoundType.ButtonClick);
        UIElementHandler.Instance.GetCanvas(id).gameObject.SetActive(true);
        UIElementHandler.Instance.GetCanvas("#MainMenu").gameObject.SetActive(false);
        UIManager.Instance.CallOnCanvasLoaded(id);
	}

    public void BackToMainMenu(string leftId)
    {
        SoundManager.Instance.Play(SoundList.UI, SoundType.ButtonClick);
        
        if(leftId == "#Settings")
            SettingsManager.Instance.SaveSettings();
        
		UIElementHandler.Instance.GetCanvas("#MainMenu").gameObject.SetActive(true);
        UIElementHandler.Instance.GetCanvas(leftId).gameObject.SetActive(false);
    }

    public void BuySkin(SkinCard skin)
    {
        switch ((int)skin.SkinCardScriptableObject.Skin.state)
        {
            case 0:
                // get all Coins and compare them with skin price
                if(LevelManager.Instance.GetAllCoins() >= skin.SkinCardScriptableObject.Skin.price)
                {skin.UpdateSkinState(SkinState.Unlocked);}
                else
                {
                    SkinManager.Instance.CanNotCollectSkin();
                }
                return;
            case >= 0:
            {
                // get skin that was selected before button press, deselect and update it
                var oldSkin = SkinManager.Instance.GetCurrentSelectedSkin();
                oldSkin.Skin.state = SkinState.Unlocked;
                oldSkin.TriggerOnSkinChanged();
            
                skin.UpdateSkinState(SkinState.Selected);
                break;
            }
        }
    }

    public void ResetGameData()
    {
        UIManager.Instance.ShowResetGameDataDialog();
    }

    public void CancelResetGameData()
    {
        UIManager.Instance.HideResetGameDataDialog();
    }

    public void ContinueResetGameData()
    {
        print("Deleting Game data ...");
        DataPersistence.Instance.DeleteGameData();
        UIManager.Instance.HideResetGameDataDialog();
    }
}
