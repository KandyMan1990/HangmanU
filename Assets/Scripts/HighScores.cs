using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public static class HighScores
{
    static string dataFile = "scores.txt";
    static List<int> ScoreList = LoadScores();

    public static void AddToList(int score)
    {
        if (ScoreList == null)
            ScoreList = new List<int>();

        ScoreList.Add(score);
        SaveScores();
    }

    static void SaveScores()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + dataFile);

        bf.Serialize(file, ScoreList);

        file.Close();

    }

    public static List<int> LoadScores()
    {
        if(File.Exists(Application.persistentDataPath + "/" + dataFile))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/" + dataFile);

            ScoreList = (List<int>)bf.Deserialize(file);
            file.Close();

            return ScoreList;
        }
        return null;
    }
}