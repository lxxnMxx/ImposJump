using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("IsGrounded Raycast stuff")]
    [SerializeField] private float castDistance;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask layerMask;
    
    private Rigidbody2D _rb;
    private PlayerBase _playerBase;
    private PlayerCollider _playerCollider;

    private bool _canMove = true;
    private float _moveDirection;


    private void OnEnable()
    {
        GameManager.Instance.OnLevelFinished += LevelFinished;
        GameManager.Instance.OnGameStart += StartGame;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelFinished -= LevelFinished;
        GameManager.Instance.OnGameStart -= StartGame;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerBase = GetComponent<PlayerBase>();
        _playerCollider = GetComponent<PlayerCollider>();
    }
    
    // if the Player falls of a Platform - dies (Code is in CameraFollowControl.cs)
    private void Update()
    {
        _moveDirection = Input.GetAxis("Horizontal");
        
        if (_moveDirection < 0 && _canMove)
        { 
            transform.Translate(Vector3.left * _playerBase.GetBaseValues(CharacterStats.MoveSpeed) * Time.deltaTime);
        } 
        if (_moveDirection > 0 && _canMove) 
        { 
            transform.Translate(Vector3.right * _playerBase.GetBaseValues(CharacterStats.MoveSpeed) * Time.deltaTime);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && _canMove)
        {
            SoundManager.Instance.Play(SoundList.Player, SoundType.PlayerJump);
            _rb.AddForce(new Vector2(0, _playerCollider.gravityDirection) * _playerBase.GetBaseValues(CharacterStats.JumpForce), ForceMode2D.Impulse);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)transform.position - Vector2.down * castDistance, boxSize);
    }

    private bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, castDistance, layerMask)) return true;
        return false;
    }

    private void LevelFinished()
    {
        _canMove = false;
    }

    private void StartGame()
    {
        _canMove = true;
    }
}
