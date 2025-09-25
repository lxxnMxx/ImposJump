using System;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerBase : MonoBehaviour
{
    [SerializeField] private Vector2 yRange;
    
    [Header("=== Player Data ===")]
    [SerializeField] private PlayerData playerData;
    
    [Space(7)]
    // particle system stuff
    [SerializeField] ParticleSystem playerDeathParticle;
    private ParticleSystem _ps;
    private ParticleSystem _component;

    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += Die;
        playerData.OnPlayerColorChanged += SetPlayerColor;
        SetPlayerColor(); // if the player directly loads into a level without opening the shop the set the player color
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= Die;
        playerData.OnPlayerColorChanged -= SetPlayerColor;
    }

    private void Update()
    {
        if(transform.position.y <= yRange.x || transform.position.y >= yRange.y)
            GameManager.Instance.ChangeGameState(GameState.GameOver);
    }

    private void SetPlayerColor()
    {
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = playerData.PlayerColor;
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
