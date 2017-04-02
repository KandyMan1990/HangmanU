using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class ShowNewGameSelect : MonoBehaviour
{
    [SerializeField] GameObject gameTypeSelect;
    [SerializeField] BlurOptimized blur;
    [SerializeField] AudioClip selectSound;
    [SerializeField] AudioClip cancelSound;
    [SerializeField]
    CanvasGroup canvasGroup;

    bool selectScreenEnabled = false;

    public void onClick()
    {
        selectScreenEnabled = !selectScreenEnabled;

        SFXManager.Instance.PlaySFX(blur.enabled ? cancelSound : selectSound);

        StartCoroutine(ShowSelectScreen());
    }

    IEnumerator ShowSelectScreen()
    {
        if (selectScreenEnabled)
        {
            blur.downsample = 0;
            blur.blurIterations = 1;
            blur.blurSize = 0f;
            canvasGroup.alpha = 0f;

            blur.enabled = true;
            gameTypeSelect.SetActive(true);

            while (blur.blurSize < 1f)
            {
                IncreaseByDeltaTime();
                yield return 0;
            }

            blur.downsample = 1;

            while (blur.blurSize < 2f)
            {
                IncreaseByDeltaTime();
                yield return 0;
            }

            blur.blurIterations = 2;

            while (blur.blurSize < 3f)
            {
                IncreaseByDeltaTime();
                yield return 0;
            }

            blur.blurSize = 3f;
            canvasGroup.alpha = 1f;
        }

        else
        {
            blur.downsample = 1;
            blur.blurIterations = 2;
            blur.blurSize = 3f;
            canvasGroup.alpha = 1f;

            while (blur.blurSize > 2f)
            {
                DecreaseByDeltaTime();
                yield return 0;
            }

            blur.blurIterations = 1;
            
            while (blur.blurSize > 1f)
            {
                DecreaseByDeltaTime();
                yield return 0;
            }

            blur.downsample = 0;

            while (blur.blurSize > 0f)
            {
                DecreaseByDeltaTime();
                yield return 0;
            }

            blur.blurSize = 0f;
            canvasGroup.alpha = 0f;

            blur.enabled = false;
            gameTypeSelect.SetActive(false);
        }
    }

    void IncreaseByDeltaTime()
    {
        blur.blurSize += Time.deltaTime * 6f;
        canvasGroup.alpha += Time.deltaTime * 3f;
    }

    void DecreaseByDeltaTime()
    {
        blur.blurSize -= Time.deltaTime * 6f;
        canvasGroup.alpha -= Time.deltaTime * 3f;
    }
}