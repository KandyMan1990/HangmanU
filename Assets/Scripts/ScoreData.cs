using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public struct ScoreData
{
    public string UserName;
    public int Score;

    public static List<ScoreData> ParseJson(string json)
    {
        try
        {
            return JsonConvert.DeserializeObject<List<ScoreData>>(json);
        }
        catch (Exception e)
        {
            return new List<ScoreData>();
        }
    }
}

public enum ScoreType
{
    GAME,
    MOVIE
}