//This script controls music, SFX and tank volume in the settings windows.

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private readonly string musicVolumeParam = "Music_Volume";
    private readonly string sfxVolumeParam = "SFX_Volume";
    private readonly string tankVolumeParam = "Tank_Volume";

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider TankSlider;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(musicVolumeParam);
        sfxSlider.value = PlayerPrefs.GetFloat(sfxVolumeParam);
        TankSlider.value = PlayerPrefs.GetFloat(tankVolumeParam);
    }

    //Called from the music slider
    public void SetMusicVolume(float value)
    {
        SetExposedParam(musicVolumeParam, value);
    }

    //Called from the SFX slider
    public void SetSFXVolume(float value)
    {
        SetExposedParam(sfxVolumeParam, value);
    }

    //Called from the tank slider
    public void SetTankVolume(float value)
    {
        SetExposedParam(tankVolumeParam, value);
    }

    public void SetExposedParam(string exposedParam, float value)
    {
        mixer.SetFloat(exposedParam, value);
        PlayerPrefs.SetFloat(exposedParam, value);
    }
}
