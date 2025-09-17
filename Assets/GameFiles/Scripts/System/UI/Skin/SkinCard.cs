
using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class SkinCard : MonoBehaviour
{
    public SkinCardScriptableObject skinCard;
    
    [Header("Sprites")]
    [SerializeField, Tooltip("Selecting sprite")]
    private Sprite selectedSkin;
    [SerializeField, Tooltip("Normal sprite (not selected & unlocked)")]
    private Sprite normalSkin;

    [SerializeField, Tooltip("Skin sprite in locked state")]
    public Sprite lockedSkin;
    
    [SerializeField, Tooltip("If there's a lock")] [CanBeNull]
    private GameObject lockObject;
    
    [Header("Necessary Refs")]
    [SerializeField]
    private Image spriteRenderer;
    
    [SerializeField, Tooltip("Player color needed")]
    private PlayerData playerData;

    private void OnEnable()
    {
        UpdateSkinState(skinCard.Skin.state);
    }

    public void UpdateSkinState(SkinState newState)
    {
        switch (newState)
        {
            case SkinState.Locked:
                spriteRenderer.sprite = lockedSkin;
                break;
            case SkinState.Unlocked:
                Unlock(skinCard.Skin.state > SkinState.Locked);
                break;
            case SkinState.Selected:
                SelectSkin();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        skinCard.Skin.state = newState;
    }

    private void SetSkinSprite(Sprite sprite) { spriteRenderer.sprite = sprite; }
    
    private void SelectSkin()
    {
        SetSkinSprite(selectedSkin);
        playerData.playerColor = skinCard.Skin.color;
    }
    
    private void Unlock(bool hasToUnlock)
    {
        SetSkinSprite(normalSkin);
        if(hasToUnlock && lockObject) lockObject.SetActive(false);
    }
}
