using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    ButtonClick,
    
    PlayerDeath,
    PlayerJump,
    PlayerJumppad,
    GravityChange,
    LevelFinished,
    CoinCollected,
    
    LaserImpact,
    AliensComing,
    AlienShoots
}

[System.Serializable]
public class Sound
{
    public SoundType type;
    public AudioClip clip;
    
    [Range(0, 1)] public float volume;
    [Range(0, 1)] public float pitch;
    
    [HideInInspector]
    public AudioSource source;
}
