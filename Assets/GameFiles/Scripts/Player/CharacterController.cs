using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerBase _playerBase;
    private PlayerCollider _playerCollider;

    private bool _canMove = true;
    private int _gravityDirection;
    private float _moveDirection;
    private bool _isInverted;
    private bool _isGrounded = true;


    private void OnEnable()
    {
        GameManager.Instance.OnLevelFinished += LevelFinished;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelFinished -= LevelFinished;
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
        
        if (Input.GetKey(KeyCode.Space) && _isGrounded && _canMove)
        {
            _isGrounded = false;
            _rb.AddForce(new Vector2(0, _playerCollider.gravityDirection) * _playerBase.GetBaseValues(CharacterStats.JumpForce), ForceMode2D.Impulse);
        }
    }

    private void LevelFinished() => _canMove = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform")) _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform")) _isGrounded = false;
    }
}
