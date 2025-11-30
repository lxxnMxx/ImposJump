using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="Scriptable Objects/UI/SkinCard", fileName="SkinCard")]
public class SkinCardScriptableObject : ScriptableObject
{
    public event Action<Skin> OnSkinPropertyChanged;

    [SerializeField, Header("Skin settings")]
    public Skin skin;

    public void TriggerOnSkinChanged() { OnSkinPropertyChanged?.Invoke(skin); }
}