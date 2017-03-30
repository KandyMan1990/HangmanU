using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WordOver : MonoBehaviour
{
    public Text State;
    public Text Word;
    public Image image;

    // Use this for initialization
    void Start()
    {
        Word.text = "The word was: " + GameManager.Instance.CurrentWord;

        if (GameManager.Instance.LivesRemaining > 0)
        {
            State.text = "Congratulations!";
            image.enabled = false;
            Invoke("Unload", 3f);
        }
        else
        {
            State.text = "Sorry!  Your score was " + GameManager.Instance.Score;
            image.enabled = true;
            Invoke("MainMenu", 3f);
        }
    }

    void Unload()
    {
        SceneManager.UnloadSceneAsync("FinishedWord");
    }

    void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}