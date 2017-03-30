using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string SceneName;
    public AudioClip ClickSound;
    
    public void MoveToScene(bool IsQuitButton)
    {
        SFXManager.Instance.PlaySFX(ClickSound);

        if(!IsQuitButton)
            SceneManager.LoadSceneAsync(SceneName);
        else
        {
            StartCoroutine(IsQuitting());
        }
    }

    IEnumerator IsQuitting()
    {
        while (SFXManager.Instance.IsPlaying)
        {
            yield return null;
        }

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}