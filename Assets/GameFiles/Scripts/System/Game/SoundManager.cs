using System;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private Sound[] sounds;
    
    void Awake()
    {
        /*
        var soundManager = GameObject.Find("SoundManager");
        if (soundManager != null && soundManager != gameObject)
        {
            Destroy(soundManager);
            return;
        }
        */
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    async public void Play(SoundType soundtype)
    {
        var s = Array.Find(sounds, sound => sound.type == soundtype);
        s.source.Play();
    }
}
