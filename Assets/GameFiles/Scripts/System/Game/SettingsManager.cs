using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : Singleton<SettingsManager>, IDataPersistence
{
    public Settings settings;

	private Slider _sfxGlobal;
	private Slider _sfxUiGlobal;


	private void OnEnable()
	{
		UIManager.Instance.OnCanvasLoad += SetSlider;
	}
	private void OnDisable()
	{
		UIManager.Instance.OnCanvasLoad -= SetSlider;
	}

	void IDataPersistence.LoadData(GameData data)
	{
		settings = data.settings;
		SetVolume();
	}

	void IDataPersistence.SaveData(ref GameData data)
	{
		data.settings = settings;
	}
	
	public void SaveSettings()
	{
		settings.sfxGloabalVolume = _sfxGlobal.value;
		settings.uiGloabalVolume = _sfxUiGlobal.value;
		print("Settings saved");
		SetVolume();
	}
	
	private void SetSlider(string cnvsName)
	{
		if (cnvsName == "#Settings")
		{
			_sfxGlobal = UIElementHandler.Instance.GetSlider("#sfxGlobal");
			_sfxUiGlobal = UIElementHandler.Instance.GetSlider("#sfxUi");
			LoadSettings();
		}
	}

	private void LoadSettings()
	{
		_sfxGlobal.value = settings.sfxGloabalVolume;
		_sfxUiGlobal.value = settings.uiGloabalVolume;
	}

	private void SetVolume()
	{
        if (SoundManager.Instance == null) return;
        foreach(Sound s in SoundManager.Instance.uiSounds)
        {
            if (s.source != null)
                s.source.volume = s.volume * settings.uiGloabalVolume;
        }
        foreach(Sound s in SoundManager.Instance.playerSounds)
        {
            if (s.source != null)
                s.source.volume = s.volume * settings.sfxGloabalVolume;
        }
        foreach(Sound s in SoundManager.Instance.alienSounds)
        {
            if (s.source != null)
                s.source.volume = s.volume * settings.sfxGloabalVolume;
        }
	}
}
