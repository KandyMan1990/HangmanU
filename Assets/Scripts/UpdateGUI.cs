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

    // Use this for initialization
    void Start()
    {
        Lives.text += GameManager.Instance.LivesRemaining.ToString();
        Score.text += GameManager.Instance.Score.ToString();
    }
}