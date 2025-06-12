using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private int _gravityDirection;
    private float _moveDirection;
    private bool _isInverted;
    private bool _isGrounded = true;
    

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
        
        if (Input.GetKey(KeyCode.Space) && IsGrounded() && _canMove)
        {
            _isGrounded = false;
            _rb.AddForce(new Vector2(0, _playerCollider.gravityDirection) * _playerBase.GetBaseValues(CharacterStats.JumpForce), ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, castDistance, layerMask))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)transform.position - Vector2.down * castDistance, boxSize);
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform") && _rb.linearVelocityY <= 2)
    //         _isGrounded = true;
    // }
    //
    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform")) _isGrounded = false;
    // }
}
