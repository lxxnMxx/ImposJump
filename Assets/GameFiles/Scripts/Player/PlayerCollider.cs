using System;
using System.Security;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private ParticleSystem finishEffect;
    [SerializeField] private PlayerData playerData;
    
    private Rigidbody2D _rb;
    private ParticleSystem _component;
    private ParticleSystem _ps;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("JumpBoost") && _rb.linearVelocityY <= 0.1) // linearVelY means if the rb moves at thy y axe
        {
            SoundManager.Instance.Play(SoundList.Player, SoundType.PlayerJumppad);
            _rb.AddForce(Vector2.up * playerData.JumpForce * 1.2f * playerData.gravityDirection, ForceMode2D.Impulse);
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
            SoundManager.Instance.Play(SoundList.Player, SoundType.GravityChange);
            playerData.gravityDirection = other.gameObject.GetComponent<GravityPad>().gravityDirection;
            _rb.gravityScale = playerData.gravityDirection < 0 ? -2.7f : 2.7f;
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            _ps = Instantiate(finishEffect, transform.position, Quaternion.identity);
            _component = _ps.gameObject.GetComponent<ParticleSystem>();
            Destroy(_ps.gameObject, _component.main.duration + _component.startLifetime);
            GameManager.Instance.ChangeGameState(GameState.LevelFinished);
        }
        
        if(other.gameObject.CompareTag("Laser")) GameManager.Instance.ChangeGameState(GameState.GameOver);
    }
}
