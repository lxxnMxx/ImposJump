using System;
using System.Security;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public int gravityDirection;
    
    private Rigidbody2D _rb;
    private PlayerBase _playerBase;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerBase = GetComponent<PlayerBase>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            GameManager.Instance.ChangeGameState(GameState.GameOver);
        }
        
        if (other.gameObject.CompareTag("JumpBoost") && _rb.linearVelocityY <= 0)
        {
            _rb.AddForce(Vector2.up * _playerBase.GetBaseValues(CharacterStats.JumpForce)*1.2f*gravityDirection, ForceMode2D.Impulse);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GravityPad"))
        {
            // check if the gravity is bigger or smaller than 0; turn the gravityDire into the opposite Value 
            if (gravityDirection is < 0 or > 0) gravityDirection = -gravityDirection;
            _rb.gravityScale *= -1;
        }
    }
}
