using System;
using UnityEngine;

public enum SoundList
{
    Player,
    Alien,
    UI
}

public class SoundManager : Singleton<SoundManager>
{
    public Sound[] uiSounds;
	public Sound[] playerSounds;
    public Sound[] alienSounds;

    private Sound _s;
    
    void Awake()
    {
		foreach (Sound s in uiSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        foreach (Sound s in playerSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        foreach (Sound s in alienSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    async public void Play(SoundList list, SoundType soundtype)
    {
        switch (list)
        {
            case SoundList.Player:
                _s = Array.Find(playerSounds, sound => sound.type == soundtype);
                break;
            case SoundList.Alien:
                _s = Array.Find(alienSounds, sound => sound.type == soundtype);
                break;
            case SoundList.UI:
                _s = Array.Find(uiSounds, sound => sound.type == soundtype);
                break;
            default:
                Debug.LogError($"Soundlist entry: {list} does not exist!");
                break;
        }
        
        if(_s == null)
        {
            Debug.LogError($"haven't found sound: {_s}!");
            return;
        }
        _s.source.Play();
    }

    public void SetVolume(Settings set)
    {
        foreach (Sound s in uiSounds)
        {
            s.source.volume = s.volume * set.uiGloabalVolume;
		}
		foreach(Sound s in playerSounds)
		{
			s.source.volume = s.volume * set.sfxGloabalVolume;
		}
		foreach(Sound s in alienSounds)
		{
			s.source.volume = s.volume * set.sfxGloabalVolume;
		}
	}
}
