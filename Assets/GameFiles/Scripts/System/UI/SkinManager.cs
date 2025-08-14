using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class SkinManager : Singleton<SkinManager>, IDataPersistence
{
    public List<Skin> skins;

    [SerializeField] private GameObject playerPrefab;
    
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
        foreach (Skin skin in skins)
        {
	        if (skin.isCollected)
		        SelectSkin(skin);
        }
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

    public void SelectSkin(Skin skin)
    {
        skin.color.a = 1; // not 255 because these values represent the percentages of each color part (1 is the maximum)
        playerPrefab.GetComponent<SpriteRenderer>().color = skin.color;

        Skin selectedSkinObj = skins.Find(s => s.isSelected);
        if (selectedSkinObj != null)
        {
	        GameObject.Find(selectedSkinObj.name).GetComponent<Image>().sprite = normalSkin;
	        selectedSkinObj.isSelected = false;
        }

        GameObject currentSkin = GameObject.Find(skin.name);
        currentSkin.GetComponent<Image>().sprite = selectedSkin;
        skin.isSelected = true;
        
        print($"skin got selected with (R: {skin.color.r}, G: {skin.color.g}, B: {skin.color.b}, A: {skin.color.a}) color");
    }

    public Skin GetSkin(string skinName) => skins.Find(s => s.name == skinName);

	private void LoadShop(string id)
	{
        print($"Loading canvas with id: {id}");
		if(id != "#Shop") return;

		foreach(Skin skin in skins)
		{
			if(!skin.isCollected && skin.name != "StandardSkin")
			{
				print(skin.name);
				GameObject skinObj = GameObject.Find(skin.name);
				skinObj.GetComponent<Image>().sprite = lockedSkin;
				skinObj.transform.GetChild(0).gameObject.SetActive(true);
			}
			else
			{
				if (skin.name == "StandardSkin")
				{
					print($"Skip standard skin");
				}
				else
					print($"no skin with name {skin.name} found!");
			}
		}
	}
}