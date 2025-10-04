using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Player data")]
    [SerializeField] private PlayerData playerData;
    
    [SerializeField] private Rigidbody2D rb;

    private bool _canMove = true;
    private float _moveDirection;


    private void OnEnable()
    {
        GameManager.Instance.OnLevelFinished += LevelFinished;
        GameManager.Instance.OnGameStart += StartGame;
        
        playerData.gravityDirection = 1;
        rb.linearVelocityY = 0;
        rb.gravityScale = 2.7f;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelFinished -= LevelFinished;
        GameManager.Instance.OnGameStart -= StartGame;
    }
    
    // if the Player falls of a Platform - dies (Code is in CameraFollowControl.cs)
    private void Update()
    {
        _moveDirection = Input.GetAxis("Horizontal");
        
        switch (_moveDirection)
        {
            case < 0 when _canMove:
                transform.Translate(Vector3.left * playerData.MoveSpeed * Time.deltaTime);
                break;
            case > 0 when _canMove:
                transform.Translate(Vector3.right * playerData.MoveSpeed * Time.deltaTime);
                break;
        }

        if (!Input.GetKeyDown(KeyCode.Space) || !IsGrounded() || !_canMove ||
            GameManager.Instance.GameState == GameState.PauseMenu) return;
        
        SoundManager.Instance.Play(SoundList.Player, SoundType.PlayerJump);
        rb.AddForce(new Vector2(0, playerData.gravityDirection) * playerData.JumpForce, ForceMode2D.Impulse);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)transform.position - Vector2.down * 
            playerData.CastDistance, playerData.BoxSize);
    }

    private bool IsGrounded() => Physics2D.BoxCast
    (transform.position, playerData.BoxSize, 0,
            Vector2.down, playerData.CastDistance, playerData.LayerMask);
    

    private void LevelFinished()
    {
        _canMove = false;
    }

    private void StartGame()
    {
        playerData.gravityDirection = 1;
        rb.linearVelocityY = 0;
        rb.gravityScale = 2.7f;
        _canMove = true;
    }
}
