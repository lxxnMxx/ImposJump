using System;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : Singleton<SkinManager>, IDataPersistence
{
    [SerializeField]
    private List<SkinCardScriptableObject> skinCards;

    private void OnEnable()
    {
        skinCards.Find(skin => skin.Skin.name == "StandardSkin").Skin.state = SkinState.Selected;
    }

    public void LoadData(GameData data)
    {
        // print("Loading skin cards");
        for (var i = 0; i < data.skins.Count; i++)
        {
            // print($"Loading Skin: {skinCards[i].Skin.name}");
            skinCards[i].Skin = data.skins[i];
        }
    }
    
    public void SaveData(ref GameData data)
    {
        for (var i = 0; i < data.skins.Count; i++)
        {
            data.skins[i] = skinCards[i].Skin;
        }
    }
    
    public SkinCardScriptableObject GetCurrentSelectedSkin() 
        => skinCards.Find(skin => skin.Skin.state == SkinState.Selected); 
}