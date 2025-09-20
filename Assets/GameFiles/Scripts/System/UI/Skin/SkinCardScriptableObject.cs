using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="Scriptable Objects/UI/SkinCard", fileName="SkinCard")]
public class SkinCardScriptableObject : ScriptableObject
{
    public event Action<Skin> OnSkinPropertyChanged;

    [SerializeField, Header("Skin settings")]
    private Skin skin;
    
    public Skin Skin
    {
        get => skin;
        internal set
        {
            skin = value;
            OnSkinPropertyChanged?.Invoke(skin);
        }
    }
}