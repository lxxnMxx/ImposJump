using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class SkinManager : Singleton<SkinManager>, IDataPersistence
{
    public List<Skin> skins;

    [SerializeField] private GameObject playerPrefab;
    
    public void LoadData(GameData data)
    {
        print("Loading data in progress ...");
    }

    public void SaveData(ref GameData data)
    {
        print("Saving data in progress ...");
    }
    
    public void CollectSkin(string skinName, Color color)
    {
        Skin skin = GetSkin(skinName);
        GameObject.Find(skinName).transform.GetChild(0).gameObject.SetActive(false); // deactivate the Image that grays out the skin
        skin.isCollected = true;
        SelectSkin(color);
    }

    public void SelectSkin(Color color)
    {
        color.a = 1; // not 255 because these values represent the percentages of each color part (1 is the maximum)
        playerPrefab.GetComponent<SpriteRenderer>().color = color;
        print($"skin got collected with (R: {color.r}, G: {color.g}, B: {color.b}, A: {color.a}) color");
    }

    public Skin GetSkin(string skinName) => skins.Find(s => s.name == skinName);
}