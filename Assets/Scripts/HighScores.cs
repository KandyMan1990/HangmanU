using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System;

[System.Serializable]
public static class HighScores
{
    static string dataFile = "scores.txt";
    static string moviesDataFile = "movies_scores.txt";
    static List<int> ScoreList = LoadScores(dataFile);
    static List<int> MoviesScoreList = LoadScores(moviesDataFile);

    private static readonly HttpClient client = new();
    private const string GAME_ENDPOINT = "http://hangmanu-game.kandykave.com/";
    private const string MOVIE_ENDPOINT = "http://hangmanu-movie.kandykave.com/";

    public static void AddToList(int score, ScoreType scoreType)
    {
        if (scoreType == ScoreType.GAME)
        {
            if (ScoreList == null)
                ScoreList = new List<int>();

            ScoreList.Add(score);
        }
        else if (scoreType == ScoreType.MOVIE)
        {
            if (MoviesScoreList == null)
                MoviesScoreList = new List<int>();

            MoviesScoreList.Add(score);
        }

        var filename = scoreType == ScoreType.GAME ? dataFile : moviesDataFile;
        SaveScores(filename);        
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

    public static async Task<List<ScoreData>> LoadScoresFromDB(ScoreType scoreType)
    {
        var endpoint = scoreType == ScoreType.GAME ? GAME_ENDPOINT : MOVIE_ENDPOINT;

        var json = await client.GetStringAsync(endpoint);

        return ScoreData.ParseJson(json);
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