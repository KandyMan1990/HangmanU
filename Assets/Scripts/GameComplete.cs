using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameComplete : MonoBehaviour
{
    public Text text;
    public Text userName;

    int score;
    ScoreType scoreType;

    void Start()
    {
        score = GameManager.Instance.Score;
        scoreType = GameManager.Instance.ActiveScoreType;
        text.text = $"Your score was {score}!";
    }

    public void SubmitScore()
    {
        StartCoroutine(HighScores.SetScore(score, userName.text, scoreType, () =>
        {
            SceneManager.LoadSceneAsync(0);
        }));
    }
}