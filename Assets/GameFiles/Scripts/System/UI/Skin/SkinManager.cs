using System;
using System.Collections.Generic;
using System.Linq;
using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class SkinManager : Singleton<SkinManager>, IDataPersistence
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private ShakeData shakeData;
    
    [Header("Skin cards")]
    [SerializeField] private List<SkinCardScriptableObject> skinCards;

    public void LoadData(GameData data)
    {
        for (var i = 0; i < data.skins.Count; i++)
        {
            skinCards[i].Skin = data.skins[i];
        }
        SelectStandardSkin();
    }
    
    public void SaveData(ref GameData data)
    {
        foreach (var skinCard in skinCards.Where(skinCard => skinCard.Skin.state == SkinState.Selected))
        {
            skinCard.Skin.state = SkinState.Unlocked;
        }

        for (var i = 0; i < data.skins.Count; i++)
        {
            data.skins[i] = skinCards[i].Skin;
        }
    }
    
    public SkinCardScriptableObject GetCurrentSelectedSkin()
        => skinCards.Find(skin => skin.Skin.state == SkinState.Selected);

    public void CanNotCollectSkin() { CameraShakerHandler.Shake(shakeData);}

    private void SelectStandardSkin()
    {
        var standardSkin = skinCards.Find(skin => skin.Skin.name == "StandardSkin").Skin;
        standardSkin.state = SkinState.Selected;
        playerData.PlayerColor =  standardSkin.color;
    }
}