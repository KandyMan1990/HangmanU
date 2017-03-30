using UnityEngine;
using UnityEngine.UI;

public class ButtonInput : MonoBehaviour
{
    public char Letter;
    Button btn;
 
    // Use this for initialization
    void Start()
    {
        GetComponentInChildren<Text>().text = Letter.ToString();
        btn = GetComponent<Button>();
    }

    public void Clicked()
    {
        //send Letter to game manager for processing
        btn.interactable = false;
        GameManager.Instance.ClickedInput(Letter);
    }
}