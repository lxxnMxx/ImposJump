using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class SkinCard : MonoBehaviour
{
    [field:SerializeField]
    public SkinCardScriptableObject SkinCardScriptableObject { get; private set; }
    
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
        SkinCardScriptableObject.OnSkinPropertyChanged += OnSkinChanged;
        UpdateSkinState(SkinCardScriptableObject.Skin.state);
    }
    private void OnDisable()
    {
        SkinCardScriptableObject.OnSkinPropertyChanged -= OnSkinChanged;
    }

    public void UpdateSkinState(SkinState newState)
    {
        SkinCardScriptableObject.Skin.state = newState;
        switch (SkinCardScriptableObject.Skin.state)
        {
            case SkinState.Locked:
                spriteRenderer.sprite = lockedSkin;
                break;
            case SkinState.Unlocked:
                Unlock(true);
                break;
            case SkinState.Selected:
                SelectSkin();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnSkinChanged(Skin skin)
    {
        if (skin.name != SkinCardScriptableObject.Skin.name) return;
        UpdateSkinState(SkinCardScriptableObject.Skin.state);
    }

    private void SetSkinSprite(Sprite sprite) { spriteRenderer.sprite = sprite; }
    
    private void SelectSkin()
    {
        SetSkinSprite(selectedSkin);
        playerData.PlayerColor = SkinCardScriptableObject.Skin.color;
        if (lockObject) lockObject.SetActive(false);
    }
    
    private void Unlock(bool hasToUnlock)
    {
        SetSkinSprite(normalSkin);
        if(hasToUnlock && lockObject) lockObject.SetActive(false);
    }
}
