using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class SkinManager : Singleton<SkinManager>, IDataPersistence
{
    public List<Skin> skins;

    [SerializeField] private GameObject playerPrefab;


	private void OnEnable()
	{
		ButtonManager.Instance.OnCanvasLoad += LoadShop;
	}

    private void OnDisable()
    {
        ButtonManager.Instance.OnCanvasLoad -= LoadShop;
	}

	public void LoadData(GameData data)
    {
        skins = data.skins;
    }

    public void SaveData(ref GameData data)
    {
        data.skins = skins;
    }
    
    public void CollectSkin(string skinName, Color color)
    {
        Skin skin = GetSkin(skinName);
        GameObject.Find(skinName).transform.GetChild(0).gameObject.SetActive(false); // deactivate the Image that grays out the skin
        skin.isCollected = true;
        print($"skin {skinName} got collected");
		SelectSkin(color);
    }

    public void SelectSkin(Color color)
    {
        color.a = 1; // not 255 because these values represent the percentages of each color part (1 is the maximum)
        playerPrefab.GetComponent<SpriteRenderer>().color = color;
        print($"skin got selected with (R: {color.r}, G: {color.g}, B: {color.b}, A: {color.a}) color");
    }

    public Skin GetSkin(string skinName) => skins.Find(s => s.name == skinName);

	private void LoadShop(string id)
	{
        print($"Loading canvas with id: {id}");
		if(id != "#Shop") return;

		foreach(Skin skin in skins)
		{
			if(skin.isCollected && skin.name != "StandardSkin")
			{
				print(skin.name);
				GameObject.Find(skin.name).transform.GetChild(0).gameObject.SetActive(false);
				continue;
			}
			print($"no skin with name {skin.name} found!");
		}
	}
}