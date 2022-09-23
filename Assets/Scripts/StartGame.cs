using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] WordDatabase words;

    public void startGame(bool isGame)
    {
        if (isGame)
        {
            GameManager.Instance.SetWords(words, ScoreType.GAME, DatabaseType.GAMES);
        }
        else
        {
            GameManager.Instance.SetWords(words, ScoreType.MOVIE, DatabaseType.MOVIES);
        }
        
    }
}