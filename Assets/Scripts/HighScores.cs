using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

[System.Serializable]
public static class HighScores
{
    private static readonly HttpClient client = new();
    private const string GetScoresEndpoint = "http://kandykave.com:18080/getscores";
    private const string SetScoresEndpoint = "http://kandykave.com:18080/setscore";

    public static async Task AddScoreToDB(int score, string name, ScoreType scoreType)
    {
        var mode = scoreType == ScoreType.GAME ? 0 : 1;
        var data = new Dictionary<string, object>
        {
            {"mode", mode},
            {"username", name },
            {"score", score }
        };

        var request = JsonConvert.SerializeObject(data).ToLower();
        var content = new HttpRequestMessage(HttpMethod.Post, SetScoresEndpoint)
        {
            Content = new StringContent(request, Encoding.UTF8, "application/json")
        };

        var response = await client.SendAsync(content);
    }

    public static async Task<List<ScoreData>> LoadScoresFromDB(ScoreType scoreType)
    {
        var mode = scoreType == ScoreType.GAME ? 0 : 1;
        var data = new Dictionary<string, int>
        {
            {"mode", mode}
        };
        var request = JsonConvert.SerializeObject(data);
        var message = new HttpRequestMessage(HttpMethod.Post, GetScoresEndpoint)
        {
            Content = new StringContent(request, Encoding.UTF8, "application/json")
        };

        var response = await client.SendAsync(message);
        var json = await response.Content.ReadAsStringAsync();

        return ScoreData.FromJson(json);
    }
}