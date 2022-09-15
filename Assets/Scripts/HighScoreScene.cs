using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HighScoreScene : MonoBehaviour
{
    [SerializeField] Text Title;
    [SerializeField] GameObject ScorePrefab;
    [SerializeField] Transform Panel;

    List<int> list;

    void Start()
    {
        Title.text = $"{GameManager.Instance.DatabaseType} High Scores";
        LoadScores();
    }

    void LoadScores()
    {
        list = HighScores.LoadScores(GameManager.Instance.ActiveFilename);

        if (list == null || list.Count == 0)
        {
            var textObj = Instantiate(ScorePrefab, Panel).GetComponent<Text>();
            textObj.text = "Failed to load high scores";
            return;
        }

        for (var i = 0; i < list.Count; i++)
        {
            var score = Instantiate(ScorePrefab, Panel).GetComponent<Text>();
            score.text = $"{i + 1}. username - {list[i]}";
        }
    }
}