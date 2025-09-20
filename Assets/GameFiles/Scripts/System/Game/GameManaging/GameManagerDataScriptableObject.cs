using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/GameManager")]
public class GameManagerDataScriptableObject : ScriptableObject 
{
    [Header("Game Manager Data")]
    public GameManagerData gameManagerData;
}
