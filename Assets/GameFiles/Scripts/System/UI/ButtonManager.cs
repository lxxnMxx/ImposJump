using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonManager : Singleton<ButtonManager>
{
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
        {
            SettingsManager.Instance.SaveSettings();
		}
		UIElementHandler.Instance.GetCanvas("#MainMenu").gameObject.SetActive(true);
        UIElementHandler.Instance.GetCanvas(leftId).gameObject.SetActive(false);
    }

    public void BuySkin(string productName) // productName has to match with the name of the actual button object
    {
        var coins = LevelManager.Instance.GetAllCoins();
        Skin currentSkin = SkinManager.Instance.GetSkin(productName);
        
        if(currentSkin.isCollected)
        {
            SkinManager.Instance.SelectSkin(currentSkin);
            print("you already own this skin");
			return;
		}

		if (coins >= currentSkin.price)
        {
            if (!currentSkin.isCollected)
            {
                SkinManager.Instance.CollectSkin(productName);
                print("You collected this skin");
                return;
            }
            SkinManager.Instance.SelectSkin(currentSkin);
        }
        else
        {
            print("you can't buy this skin, get some more coins");
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
