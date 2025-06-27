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
        if (other.gameObject.CompareTag("JumpBoost") && _rb.linearVelocityY <= 0.1) // linearVelY means if the rb moves at thy y axe
        {
            SoundManager.Instance.Play(SoundType.PlayerJumppad);
            _rb.AddForce(Vector2.up * _playerBase.GetBaseValues(CharacterStats.JumpForce) * 1.2f * gravityDirection, ForceMode2D.Impulse);
        }

        if (other.gameObject.CompareTag("Spike") || other.gameObject.CompareTag("Alien"))
        {
            GameManager.Instance.ChangeGameState(GameState.GameOver);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GravityPad"))
        {
            SoundManager.Instance.Play(SoundType.GravityChange);
            gravityDirection = other.gameObject.GetComponent<GravityPad>().gravityDirection;
            _rb.gravityScale = gravityDirection < 0 ? -2.7f : 2.7f;
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.Instance.ChangeGameState(GameState.LevelFinished);
        }
        
        if(other.gameObject.CompareTag("Laser")) GameManager.Instance.ChangeGameState(GameState.GameOver);
    }
}
