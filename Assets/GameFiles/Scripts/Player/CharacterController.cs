using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [Space(7)]
    [Range(-5, -40)] [SerializeField] float minHeight;
    
    private Rigidbody2D _rb;
    private PlayerBase _playerBase;
    private PlayerCollider _playerCollider;

    private int _gravityDirection;
    private float _moveDirection;
    private bool _isGrounded = true;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerBase = GetComponent<PlayerBase>();
        _playerCollider = GetComponent<PlayerCollider>();
    }

    private void Update()
    {
        if(transform.position.y < minHeight)
            GameManager.Instance.ChangeGameState(GameState.GameOver);
        
        _moveDirection = Input.GetAxis("Horizontal");
        
        if (_moveDirection < 0)
        { 
            transform.Translate(Vector3.left * _playerBase.GetBaseValues(CharacterStats.MoveSpeed) * Time.deltaTime);
        } 
        if (_moveDirection > 0) 
        { 
            transform.Translate(Vector3.right * _playerBase.GetBaseValues(CharacterStats.MoveSpeed) * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _isGrounded = false;
            //_gravityDirection = _playerCollider.gravityDirection;
            //print(_gravityDirection);
            _rb.AddForce(new Vector2(0, _playerCollider.gravityDirection) * _playerBase.GetBaseValues(CharacterStats.JumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground")) _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground")) _isGrounded = false;
    }
}
