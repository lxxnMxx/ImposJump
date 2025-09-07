using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SkinManager : Singleton<SkinManager>, IDataPersistence
{
    public List<Skin> skins;

    [SerializeField] private PlayerData playerData;
    
    [Header("Sprites")]
	[SerializeField] private Sprite normalSkin;
    [SerializeField] private Sprite selectedSkin;
    [SerializeField] private Sprite lockedSkin;


	private void OnEnable()
	{
		UIManager.Instance.OnCanvasLoad += LoadShop;
	}

    private void OnDisable()
    {
        UIManager.Instance.OnCanvasLoad -= LoadShop;
	}

	public void LoadData(GameData data)
	{
		skins = data.skins;
		var skin = skins.Find(s => s.name == "StandardSkin");
		SelectSkin(skin);
	}

    public void SaveData(ref GameData data)
    {
        data.skins = skins;
    }
    
    public void CollectSkin(string skinName)
    {
        var skinObject = GameObject.Find(skinName); 
        skinObject.GetComponent<Image>().sprite = selectedSkin;
        skinObject.transform.GetChild(0).gameObject.SetActive(false);
        Skin skin = GetSkin(skinName);
        skin.isCollected = true;
        print($"skin {skinName} got collected");
		SelectSkin(skin);
    }

    public async void SelectSkin(Skin skin)
    {
	    var isMainMenu =  await WaitForMainMenu();
        skin.color.a = 1; // not 255 because these values represent the percentages of each color part (1 is the maximum)

        Skin selectedSkinObj = skins.Find(s => s.isSelected);
        if (selectedSkinObj != null)
        {
	        var skinObj = GameObject.Find(selectedSkinObj.name);	
	        if (skinObj != null) skinObj.GetComponent<Image>().sprite = normalSkin;
	        selectedSkinObj.isSelected = false;
	        
	        var skinobj = GameObject.Find(skin.name); 
	        skin.isSelected = true;
	        playerData.playerColor = skin.color;
	        if(skinobj != null)
		        skinobj.GetComponent<Image>().sprite = selectedSkin;
	        else print("something went wrong SkinManager Ln 73");
        }
		else Debug.LogWarning("Selected skin not found (Line 70 SkinManager.cs)");
        
        print($"skin got selected with (R: {skin.color.r}, G: {skin.color.g}, B: {skin.color.b}, A: {skin.color.a}) color");
    }

    public Skin GetSkin(string skinName) => skins.Find(s => s.name == skinName);

	private void LoadShop(string id)
	{
        print($"Loading canvas with id: {id}");
		if(id != "#Shop") return;

		foreach(Skin skin in skins)
		{
			GameObject skinObj = GameObject.Find(skin.name);
			if(!skin.isCollected)
			{
				print(skin.name);
				skinObj.GetComponent<Image>().sprite = lockedSkin;
				skinObj.transform.GetChild(0).gameObject.SetActive(true);
			}

			if (skin.isCollected)
			{
				print($"{skin.name} is already collected");
				
				if (skin.isSelected)
					skinObj.GetComponent<Image>().sprite = selectedSkin;
				else
					skinObj.GetComponent<Image>().sprite = normalSkin;
				
				if(skinObj.name != "StandardSkin")
					skinObj.transform.GetChild(0).gameObject.SetActive(false);
			}
			else
			{
				if (skin.name == "StandardSkin")
					print($"Skip standard skin");
				else
					print($"no skin with name {skin.name} found!");
			}
		}
	}

	private async Task<bool> WaitForMainMenu()
	{
		while (GameManager.Instance.GameState != GameState.MainMenu)
		{
			await Task.Yield();
		}

		return true;
	}
}
