using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    [SerializeField]AudioMixer mixer;

    public void SetMusicVolume(float volume)
    {
        if (mixer)
        {
            float vol = 20f * Mathf.Log10(volume);
            mixer.SetFloat("MusicVolume", vol);
            PlayerPrefs.SetFloat("MusicVolume", vol);
            PlayerPrefs.SetFloat("MusicVolumeSlider", volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (mixer)
        {
            float vol = 20f * Mathf.Log10(volume);
            mixer.SetFloat("SFXVolume", vol);
            PlayerPrefs.SetFloat("SFXVolume", vol);
            PlayerPrefs.SetFloat("SFXVolumeSlider", volume);
        }
    }
}