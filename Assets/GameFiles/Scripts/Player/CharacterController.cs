using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [Header("Player data")]
    [SerializeField] private PlayerData playerData;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerInput playerInput;
    
    private bool _canMove = true;
    private Vector2 _moveDirection;
    
    // Input system
    private InputAction _moveAction;
    private InputAction _jumpAction;
    
    
    private void Awake()
    {
        _moveAction = playerInput.actions["Move"];
        _jumpAction = playerInput.actions["Jump"];
    }

    private void OnEnable()
    {
        GameManager.Instance.OnLevelFinished += LevelFinished;
        GameManager.Instance.OnGameStart += StartGame;
        GameManager.Instance.OnGameStateChange += OnPause;
        GameManager.Instance.OnGameStateChange += OnGameContinues;
        
        playerData.gravityDirection = 1;
        rb.linearVelocityY = 0;
        rb.gravityScale = 2.7f;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelFinished -= LevelFinished;
        GameManager.Instance.OnGameStart -= StartGame;
        GameManager.Instance.OnGameStateChange -= OnPause;
        GameManager.Instance.OnGameStateChange -= OnGameContinues;
    }

    private void Update()
    {
        if (!_canMove) return;
        Move();
        if (!_jumpAction.WasPerformedThisFrame() || !IsGrounded())
            return;
        Jump();
    }

    #region InputSystem

    private void Move()
    {
        _moveDirection = _moveAction.ReadValue<Vector2>();
        transform.Translate(new Vector3(_moveDirection.x, 0, 0) * (playerData.MoveSpeed * Time.deltaTime));
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, playerData.gravityDirection) * (playerData.JumpForce), ForceMode2D.Impulse);
        SoundManager.Instance.Play(SoundList.Player, SoundType.PlayerJump);
    }

    #endregion
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)transform.position - Vector2.down * 
            playerData.CastDistance, playerData.BoxSize);
    }

    private bool IsGrounded() => Physics2D.BoxCast
    (transform.position, playerData.BoxSize, 0,
            Vector2.down, playerData.CastDistance, playerData.LayerMask);


    #region EventMethods

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

    private void OnPause(GameState state)
    {
        if (state != GameState.Pause) return;
        _canMove = false;
    }

    private void OnGameContinues(GameState state)
    {
        if (state != GameState.GameContinues) return;
        _canMove = true;
    }

    #endregion
}
