using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameComplete : MonoBehaviour
{
    public Text text;

    // Use this for initialization
    void Start()
    {
        text.text = "Your score was " + GameManager.Instance.Score + "!";
        Invoke("MainMenu", 3f);
    }

    void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}