using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public struct ScoreData
{
    public string UserName;
    public int Score;

    public static List<ScoreData> FromJson(string json)
    {
        try
        {
            return JsonConvert.DeserializeObject<List<ScoreData>>(json);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return new List<ScoreData>();
        }
    }

    public static string ToJson(string username, int score)
    {
        return JsonConvert.SerializeObject(new ScoreData { UserName = username, Score = score });
    }
}
public enum ScoreType
{
    GAME,
    MOVIE
}

public enum ApiCmd
{
    NONE,
    GET,
    POST
}