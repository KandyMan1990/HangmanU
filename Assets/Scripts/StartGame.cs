using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] WordDatabase words;

    public void startGame(string filename)
    {
        GameManager.Instance.SetWords(words, filename);
    }
}