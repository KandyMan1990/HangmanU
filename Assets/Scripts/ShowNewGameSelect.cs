using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class ShowNewGameSelect : MonoBehaviour
{
    [SerializeField] GameObject gameTypeSelect;
    [SerializeField] BlurOptimized blur;
    [SerializeField] AudioClip selectSound;
    [SerializeField] AudioClip cancelSound;

    public void onClick()
    {
        blur.enabled = !blur.enabled;
        gameTypeSelect.SetActive(blur.enabled);

        SFXManager.Instance.PlaySFX(blur.enabled ? selectSound : cancelSound);
    }
}