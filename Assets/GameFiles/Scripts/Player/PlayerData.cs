using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Stats")]
    public float jumpForce;
    public float moveSpeed;
    public Color playerColor;
}
