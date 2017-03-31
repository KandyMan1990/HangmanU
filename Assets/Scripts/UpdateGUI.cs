using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGUI : MonoBehaviour
{
    public Text Lives;
    public Text Score;
    public Text Word;
    public Image image;

    public List<Button> Buttons;

    const string LIVES_REMAINING = "Score for this word: ";
    const string SCORE = "Score: ";

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

    void Start()
    {
        Lives.text += GameManager.Instance.LivesRemaining.ToString();
        Score.text += GameManager.Instance.Score.ToString();
    }

    public void Reset(char[] newWord)
    {
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

    public void UpdateUI(int livesRemaining, int score)
    {
        image.sprite = (Sprite)Resources.Load(livesRemaining.ToString(), typeof(Sprite));
        Lives.text = LIVES_REMAINING + (livesRemaining * 10).ToString();
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
}