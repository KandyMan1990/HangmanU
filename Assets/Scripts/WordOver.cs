using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Linq;

public class WordOver : MonoBehaviour
{
    [SerializeField] Text State;
    [SerializeField] Text Word;
    [SerializeField] Image image;
    [SerializeField] GameObject insertName;
    [SerializeField] Text userName;

    int score;

    void Start()
    {
        insertName.SetActive(false);

        Word.text = "The word was: " + GameManager.Instance.CurrentWord;

        if (GameManager.Instance.CurrentRoundScore > 0)
        {
            _ = ProcessWin();
            return;
        }

        ProcessLose();
    }

    async Task ProcessWin()
    {
        State.text = "Congratulations!";
        image.enabled = false;
        await Task.Delay(3000);
        SceneManager.UnloadSceneAsync("FinishedWord");
    }

    void ProcessLose()
    {
        score = GameManager.Instance.Score;
        State.text = $"Sorry!  Your score was {score}";
        image.enabled = true;

        StartCoroutine(HighScores.GetScores(GameManager.Instance.ActiveScoreType, async () =>
        {
            var scores = HighScores.GetScoresList;

            if (scores.Any(s => score >= s.Score))
            {
                insertName.SetActive(true);
                return;
            }

            await Task.Delay(3000);
            SceneManager.LoadSceneAsync(0);
        }));
    }

    public void SubmitScore()
    {
        StartCoroutine(HighScores.SetScore(score, userName.text, GameManager.Instance.ActiveScoreType, () =>
        {
            SceneManager.LoadSceneAsync(0);
        }));
    }
}