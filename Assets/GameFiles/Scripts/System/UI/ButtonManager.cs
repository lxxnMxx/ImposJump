using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonManager : Singleton<ButtonManager>
{
    private int _nextTutorialIndex;
    private string _previousTutorialIndex;
    
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
        GameManager.Instance.ChangeGameState(GameManager.Instance.lastGameState);
    }

    public void SelectLevel(int index)
    {
        SoundManager.Instance.Play(SoundList.UI, SoundType.ButtonClick);
        MainMenuManager.Instance.SelectLevel(index);
    }

    public void Play()
    {
        SoundManager.Instance.Play(SoundList.UI, SoundType.ButtonClick);
        SceneHandler.Instance.LoadLevel(MainMenuManager.Instance.GetSelectedLevel());
    }

    public void MainMenu()
    {
        SoundManager.Instance.Play(SoundList.UI, SoundType.ButtonClick);
        SceneHandler.Instance.LoadMainMenu();
    }

    public void NextTutorial()
    {
        _nextTutorialIndex = SceneHandler.Instance.tutorials.FindIndex(i => i == SceneManager.GetActiveScene().name);
        if(_nextTutorialIndex <= 3) // prevent the index from being out of bounds
            SceneHandler.Instance.LoadTutorial(SceneHandler.Instance.tutorials[_nextTutorialIndex+1]);
        else // load main menu if the index gets out of bounds
            SceneHandler.Instance.LoadMainMenu();
    }

    public void PreviousTutorial()
    {
        _nextTutorialIndex = SceneHandler.Instance.tutorials.FindIndex(i => i == SceneManager.GetActiveScene().name);
        if(_nextTutorialIndex > 1) // prevent the index from being out of bounds
            SceneHandler.Instance.LoadTutorial(SceneHandler.Instance.tutorials[_nextTutorialIndex-1]);
        else // load main menu if the index gets out of bounds
            SceneHandler.Instance.LoadMainMenu();
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
    }

    public void BackToMainMenu(string leftId)
    {
        SoundManager.Instance.Play(SoundList.UI, SoundType.ButtonClick);
        UIElementHandler.Instance.GetCanvas("#MainMenu").gameObject.SetActive(true);
        UIElementHandler.Instance.GetCanvas(leftId).gameObject.SetActive(false);
    }

    public void BuySkin(string productName) // productName has to match with the name of the actual button object
    {
        var coins = LevelManager.Instance.GetAllCoins();
        Skin currentSkin = SkinManager.Instance.GetSkin(productName);
        print(currentSkin.name);
        
        if (coins >= currentSkin.price)
        {
            if (!currentSkin.isCollected)
            {
                SkinManager.Instance.CollectSkin(productName, currentSkin.color);
                return;
            }
            SkinManager.Instance.SelectSkin(currentSkin.color);
        }
        else
        {
            print("you can't buy this skin, get some more coins");
        }
    }
}
