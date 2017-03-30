using UnityEngine;
using UnityEngine.Audio;

public class GetAudioVolumes : MonoBehaviour
{
    [SerializeField]
    AudioMixer mixer;

    void Start()
    {
        float vol = PlayerPrefs.GetFloat("MusicVolume");
        mixer.SetFloat("MusicVolume", vol);
        vol = PlayerPrefs.GetFloat("SFXVolume");
        mixer.SetFloat("SFXVolume", vol);
    }
}