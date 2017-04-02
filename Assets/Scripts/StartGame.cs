using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] WordDatabase words;

    public void startGame()
    {
        GameManager.Instance.SetWords(words);
    }
}