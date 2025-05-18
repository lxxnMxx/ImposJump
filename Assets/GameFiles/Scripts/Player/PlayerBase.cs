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
    
    [Space(7)]
    
    [SerializeField] ParticleSystem playerDeathParticle;
    
    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += Die;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= Die;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Die()
    {
        SoundManager.Instance.Play(SoundType.PlayerDeath);
        Instantiate(playerDeathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
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
}
