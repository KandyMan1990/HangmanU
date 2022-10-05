using System.Threading.Tasks;
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
        _ = Process();
    }

    public async Task Process()
    {
        await HighScores.AddScoreToDB(score, userName.text, scoreType);

        SceneManager.LoadSceneAsync(0);
    }
}