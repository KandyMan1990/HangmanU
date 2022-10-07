using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;

public class WordOver : MonoBehaviour
{
    [SerializeField] Text State;
    [SerializeField] Text Word;
    [SerializeField] Image image;
    [SerializeField] GameObject insertName;
    [SerializeField] Text userName;

    int score;
    readonly WaitForSeconds wait = new(3f);

    void Start()
    {
        insertName.SetActive(false);

        Word.text = "The word was: " + GameManager.Instance.CurrentWord;

        if (GameManager.Instance.CurrentRoundScore > 0)
        {
            ProcessWin();
            return;
        }

        ProcessLose();
    }

    void ProcessWin()
    {
        State.text = "Congratulations!";
        image.enabled = false;
        StartCoroutine(UnloadFinishedWord());
    }

    void ProcessLose()
    {
        score = GameManager.Instance.Score;
        State.text = $"Sorry!  Your score was {score}";
        image.enabled = true;

        StartCoroutine(HighScores.GetScores(GameManager.Instance.ActiveScoreType, () =>
        {
            var scores = HighScores.GetScoresList;

            if (scores.Any(s => score >= s.Score))
            {
                insertName.SetActive(true);
                return;
            }

            StartCoroutine(GoToMainMenu());
        }));
    }

    IEnumerator UnloadFinishedWord()
    {
        yield return wait;
        SceneManager.UnloadSceneAsync("FinishedWord");
    }

    IEnumerator GoToMainMenu()
    {
        yield return wait;
        SceneManager.LoadSceneAsync(0);
    }

    public void SubmitScore()
    {
        StartCoroutine(HighScores.SetScore(score, userName.text, GameManager.Instance.ActiveScoreType, () =>
        {
            SceneManager.LoadSceneAsync(0);
        }));
    }
}