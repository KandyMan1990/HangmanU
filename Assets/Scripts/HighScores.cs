using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public static class HighScores
{
    static string dataFile = "scores.txt";
    static string moviesDataFile = "movies_scores.txt";
    static List<int> ScoreList = LoadScores(dataFile);
    static List<int> MoviesScoreList = LoadScores(moviesDataFile);

    public static string GamesFilename
    {
        get { return dataFile; }
    }

    public static string MoviesFilename
    {
        get { return moviesDataFile; }
    }

    public static void AddToList(int score, string filename)
    {
        if (!string.IsNullOrEmpty(filename))
        {
            if (filename == dataFile)
            {
                if (ScoreList == null)
                    ScoreList = new List<int>();

                ScoreList.Add(score);
            }
            else if (filename == moviesDataFile)
            {
                if (MoviesScoreList == null)
                    MoviesScoreList = new List<int>();

                MoviesScoreList.Add(score);
            }

            SaveScores(filename);
        }
    }

    static void SaveScores(string filename)
    {
        if (!string.IsNullOrEmpty(filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + filename);

            if (filename == dataFile)
            {
                bf.Serialize(file, ScoreList);
            }
            else if (filename == moviesDataFile)
            {
                bf.Serialize(file, MoviesScoreList);
            }

            file.Close();
        }
    }

    public static List<int> LoadScores(string filename)
    {
        if (!string.IsNullOrEmpty(filename))
        {
            if (File.Exists(Application.persistentDataPath + "/" + filename))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.OpenRead(Application.persistentDataPath + "/" + filename);

                if (filename == dataFile)
                {
                    ScoreList = (List<int>)bf.Deserialize(file);
                    file.Close();
                    return ScoreList;
                }
                else if (filename == moviesDataFile)
                {
                    MoviesScoreList = (List<int>)bf.Deserialize(file);
                    file.Close();
                    return MoviesScoreList;
                }
            }
        }

        return null;
    }
}