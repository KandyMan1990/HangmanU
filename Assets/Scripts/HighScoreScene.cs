using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

public class HighScoreScene : MonoBehaviour
{
    [SerializeField] Text Title;
    [SerializeField] GameObject ScorePrefab;
    [SerializeField] GameObject Loading;
    [SerializeField] GameObject LoadFailed;
    [SerializeField] Transform Panel;

    List<ScoreData> list;

    async void Start()
    {
        Title.text = $"{GameManager.Instance.DatabaseType} High Scores";
        await LoadScores();
    }

    async Task LoadScores()
    {
        list = await HighScores.LoadScoresFromDB(GameManager.Instance.ActiveScoreType);

        Destroy(Loading);

        if (list.Count == 0)
        {
            LoadFailed.SetActive(true);
            return;
        }

        for (var i = 0; i < list.Count; i++)
        {
            var score = Instantiate(ScorePrefab, Panel).GetComponent<Text>();
            score.text = $"{i + 1}. {list[i].UserName} - {list[i].Score}";
        }

        Destroy(LoadFailed);
    }
}