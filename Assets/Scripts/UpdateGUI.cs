using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGUI : MonoBehaviour
{
    [SerializeField] Canvas MainCanvas;
    [SerializeField] Text Lives;
    [SerializeField] Text Score;
    [SerializeField] Text Word;
    [SerializeField] Image image;
    [SerializeField] List<Button> Buttons;

    const string CURRENT_ROUND_SCORE = "Score for this word: ";
    const string SCORE = "Score: ";

    void Start()
    {
        Lives.text += GameManager.Instance.CurrentRoundScore.ToString();
        Score.text += GameManager.Instance.Score.ToString();
    }

    public void EnableButtons(bool enabled)
    {
        foreach (Button btn in Buttons)
        {
            btn.interactable = enabled;
        }
    }

    public void DisableButton(string character)
    {
        character = character.ToUpper();

        foreach (Button btn in Buttons)
        {
            if (btn)
            {
                if (btn.name == "Btn" + character)
                    btn.interactable = false;
            }
        }
    }

    public void Reset(char[] newWord, int currentRoundScore)
    {
        image.sprite = (Sprite)Resources.Load(currentRoundScore.ToString(), typeof(Sprite));

        SetUpWord(newWord);
    }

    void SetUpWord(char[] characters)
    {
        Word.text = string.Empty;

        foreach (char c in characters)
        {
            if (char.IsWhiteSpace(c))
                Word.text += c;
            else
                Word.text += '_';
        }
    }

    public void UpdateUI(int currentRoundScore, int score)
    {
        image.sprite = (Sprite)Resources.Load(currentRoundScore.ToString(), typeof(Sprite));
        Lives.text = CURRENT_ROUND_SCORE + (currentRoundScore).ToString();
        Score.text = SCORE + score.ToString();
    }

    public char[] GetWordAsCharArray()
    {
        return Word.text.ToCharArray();
    }

    public string GetWord()
    {
        return Word.text;
    }

    public void SetWordText(string word)
    {
        Word.text = word;
    }

    public void SetCanvasActive(bool active)
    {
        MainCanvas.gameObject.SetActive(active);
    }
}