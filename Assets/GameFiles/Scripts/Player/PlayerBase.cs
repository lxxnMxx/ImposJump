using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum CharacterStats
{
    MoveSpeed,
    JumpForce
}

public class PlayerBase : MonoBehaviour
{
    [Header("=== Values ===")] 
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 yRange;
    
    [Space(7)]
    
    [SerializeField] ParticleSystem playerDeathParticle;
    
    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += Die;
        
        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= Die;
    }

    private void Update()
    {
        if(transform.position.y <= yRange.x || transform.position.y >= yRange.y)
            GameManager.Instance.ChangeGameState(GameState.GameOver);
    }
    
    public float GetBaseValues(CharacterStats value)
    {
        switch (value)
        {
            case CharacterStats.JumpForce:
                return jumpForce;
            case CharacterStats.MoveSpeed:
                return moveSpeed;
            default:
                Debug.LogError("This CharacterStat Type doesn't exist! Check your Input!");
                return 0;
        }
    }

    private void Die()
    {
        SoundManager.Instance.Play(SoundType.PlayerDeath);
        Instantiate(playerDeathParticle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
