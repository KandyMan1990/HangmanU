using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HighScoreScene : MonoBehaviour
{
    [SerializeField] Text Title;
    [SerializeField] Text Score1;
    [SerializeField] Text Score2;
    [SerializeField] Text Score3;
    [SerializeField] Text Score4;
    [SerializeField] Text Score5;

    List<int> list;

    void Start()
    {
        Load();
    }

    void Load()
    {
        Title.text = GameManager.Instance.DatabaseType + " High Scores";

        list = HighScores.LoadScores(GameManager.Instance.ActiveFilename);

        if (list != null)
        {
            if (list.Count > 0)
            {
                list.Sort();
                list.Reverse();
                if (list.Count > 5)
                    while (list.Count > 5)
                        list.RemoveAt(list.Count - 1);

                try
                {
                    Score1.text = "1. " + list[0];
                    Score2.text = "2. " + list[1];
                    Score3.text = "3. " + list[2];
                    Score4.text = "4. " + list[3];
                    Score5.text = "5. " + list[4];
                }
                catch
                {

                }
            }
        }
    }
}