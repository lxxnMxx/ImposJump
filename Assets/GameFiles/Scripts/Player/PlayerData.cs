using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Stats")]
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    public Color playerColor;
    public int gravityDirection;
    
    [field:Header("JumpingStuff")]
    [field: SerializeField] public float CastDistance { get; private set; }
    [field: SerializeField] public Vector2 BoxSize { get; private set; }
    [field: SerializeField] public LayerMask LayerMask { get; private set; }

}
