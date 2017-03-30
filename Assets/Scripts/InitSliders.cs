using UnityEngine;
using UnityEngine.UI;

public class InitSliders : MonoBehaviour
{
    [SerializeField]Slider music;
    [SerializeField]Slider sfx;

    void Start()
    {
        float vol = PlayerPrefs.GetFloat("MusicVolumeSlider");
        music.value = vol;
        vol = PlayerPrefs.GetFloat("SFXVolumeSlider");
        sfx.value = vol;
    }
}