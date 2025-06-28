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
    // particle system stuff
    [SerializeField] ParticleSystem playerDeathParticle;
    private ParticleSystem _ps;
    private ParticleSystem _component;
    
    
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
        SoundManager.Instance.Play(SoundList.Player, SoundType.PlayerDeath);
        
        // Ps stuff (instantiate and destroy)
        _ps = Instantiate(playerDeathParticle, transform.position, Quaternion.identity);
        _component = playerDeathParticle.GetComponent<ParticleSystem>();
        Destroy(_ps.gameObject, _component.startLifetime + _component.main.duration);
        
        gameObject.SetActive(false);
    }
}
