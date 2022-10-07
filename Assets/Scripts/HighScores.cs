using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine.Networking;
using System;

[System.Serializable]
public static class HighScores
{
    private static readonly HttpClient client = new();
    private const string GetScoresEndpoint = "http://kandykave.com:18080/getscores";
    private const string SetScoresEndpoint = "http://kandykave.com:18080/setscore";

    public static List<ScoreData> GetScoresList { get; private set; }

    public static IEnumerator GetScores(ScoreType scoreType, Action callback)
    {
        GetScoresList = null;
        var mode = scoreType == ScoreType.GAME ? 0 : 1;
        var data = new Dictionary<string, int>
        {
            {"mode", mode}
        };
        var json = JsonConvert.SerializeObject(data);

        using var request = new UnityWebRequest(GetScoresEndpoint, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        GetScoresList = ScoreData.FromJson(request.downloadHandler.text);
        callback();
    }

    public static IEnumerator SetScore(int score, string name, ScoreType scoreType, Action callback)
    {
        var mode = scoreType == ScoreType.GAME ? 0 : 1;
        var data = new Dictionary<string, object>
        {
            {"mode", mode},
            {"username", name },
            {"score", score }
        };
        var json = JsonConvert.SerializeObject(data).ToLower();

        using var request = new UnityWebRequest(SetScoresEndpoint, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        callback();
    }
}