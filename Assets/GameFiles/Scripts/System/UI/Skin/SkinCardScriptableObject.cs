using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="Scriptable Objects/UI/SkinCard", fileName="SkinCard")]
public class SkinCardScriptableObject : ScriptableObject
{
    public event Action<Skin> OnSkinPropertyChanged;

    [field:SerializeField, Header("Skin settings")]
    public Skin Skin { get; set; }

    public void TriggerOnSkinChanged() { OnSkinPropertyChanged?.Invoke(Skin); }
}