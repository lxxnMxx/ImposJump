using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="Scriptable Objects/UI/SkinCard", fileName="SkinCard")]
public class SkinCardScriptableObject : ScriptableObject
{
    [field: SerializeField, Header("Skin settings")]
    public Skin Skin { get; internal set; }
}