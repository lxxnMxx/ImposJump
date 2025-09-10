using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private Rigidbody2D  playerRigidBody;
    
    [Header("Stats")]
    public float jumpForce;
    public float moveSpeed;
    public Color playerColor;
}
