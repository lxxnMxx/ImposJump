using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SkinCard : MonoBehaviour, IDataPersistence
{
    [Header("Sprites")]
    [SerializeField, Tooltip("Selecting sprite")]
    private Sprite selected;

    [SerializeField, Tooltip("Normal sprite (not selected & unlocked)")]
    private Sprite normal;
    
    [SerializeField, Tooltip("If there's a lock")]
    private GameObject lockObject;
    
    [Header("Skin")]
    [field: SerializeField]
    public Skin Skin { get; private set; }
    
    [Header("Necessary Refs")]
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    
    [SerializeField]
    private PlayerData playerData;
    
    
    public void LoadData(GameData data)
    {
        Skin = data.skins.Find(s => s.name == Skin.name);
    }

    public void SaveData(ref GameData data)
    {
        data.skins[data.skins.FindIndex(s => s.name == Skin.name)] = Skin;
    }
    

    public void SelectSkin()
    {
        Skin.isSelected = true;
        spriteRenderer.sprite = selected;
        playerData.playerColor = Skin.color;
    }

    public void DeselectSkin()
    {
        Skin.isSelected = false;
        spriteRenderer.sprite = normal;
    }

    public void Unlock()
    {
        spriteRenderer.sprite = normal;
        Skin.isUnlocked = true;
        if(lockObject) lockObject.SetActive(false);
    }
}
