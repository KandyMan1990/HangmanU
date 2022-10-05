using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] WordDatabase words;

    public void startGame(bool isGame)
    {
        var scoreType = isGame ? ScoreType.GAME : ScoreType.MOVIE;

        GameManager.Instance.SetWords(words, scoreType);
    }
}