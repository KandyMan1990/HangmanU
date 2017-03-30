using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //TODO: shuffle list at beginning and pop off first index instead of using random every word
    //cleanup this script (look over every line)
    //why is dont destroy on load commented out???
    static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (GameManager)FindObjectOfType(typeof(GameManager));
                if (instance == null)
                {
                    GameObject templatePrefab = Resources.Load("GameManager") as GameObject;

                    GameObject GM;
                    if (templatePrefab != null)
                    {
                        GM = Instantiate(templatePrefab);
                    }
                    else
                    {
                        GM = new GameObject("Game Manager");
                        GM.AddComponent<GameManager>();
                    }

                    instance = GM.GetComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        Object[] GMs = FindObjectsOfType(typeof(GameManager));
        for (int i = 0; i < GMs.Length; i++)
        {
            if (GMs[i] != this)
            {
                Destroy(gameObject);
            }
        }

        //DontDestroyOnLoad(gameObject);
    }

    const string LIVES_REMAINING = "Lives Remaining: ";
    const string SCORE = "Score: ";

    int livesRemaining;
    int score;
    [SerializeField]
    string currentWord;
    List<string> usedKeys = new List<string>();
    bool canCheckState = true;

    public int LivesRemaining
    {
        get { return livesRemaining; }
    }
    public int Score
    {
        get { return score; }
    }
    public string CurrentWord { get; private set; }
    public AudioClip CorrectInput;
    public AudioClip IncorrectInput;
    public AudioClip CorrectAnswer;
    public AudioClip LostGame;
    public AudioClip WinGame;
    Text LivesUI;
    Text ScoreUI;
    Text WordUI;
    UpdateGUI GUI;

    [SerializeField]
    string[] words;

    [SerializeField]
    List<string> availableWords = new List<string>();

    // Use this for initialization
    void Start()
    {
        availableWords.AddRange(words);

        GUI = FindObjectOfType(typeof(UpdateGUI)) as UpdateGUI;
        LivesUI = GameObject.Find("UIManager").GetComponent<UpdateGUI>().Lives;
        ScoreUI = GameObject.Find("UIManager").GetComponent<UpdateGUI>().Score;
        WordUI = GameObject.Find("UIManager").GetComponent<UpdateGUI>().Word;
        score = 0;
        Reset();
    }

    void Reset()
    {
        livesRemaining = 10;
        score += 100;
        GUI.image.sprite = (Sprite)Resources.Load(livesRemaining.ToString(), typeof(Sprite));

        if (availableWords.Count > 0)
        {
            int i = Random.Range(0, availableWords.Count);
            currentWord = availableWords[i];
            availableWords.RemoveAt(i);


            char[] characters = currentWord.ToCharArray();
            WordUI.text = string.Empty;
            foreach (char c in characters)
            {
                if (char.IsWhiteSpace(c))
                    WordUI.text += c;
                else
                    WordUI.text += '_';
            }

            canCheckState = true;
            GUI.EnableButtons(true);
            usedKeys.Clear();
            usedKeys.TrimExcess();
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        GUI.image.sprite = (Sprite)Resources.Load(livesRemaining.ToString(), typeof(Sprite));
        LivesUI.text = LIVES_REMAINING + livesRemaining.ToString();
        ScoreUI.text = SCORE + score.ToString();
    }

    public void ClickedInput(char btnInput)
    {
        string s = btnInput.ToString();
        s = s.ToLower();

        if (!usedKeys.Contains(s))
        {
            usedKeys.Add(s);
            ProcessInput(btnInput);
        }
    }

    void ProcessInput(char input)
    {
        if (canCheckState)
        {
            canCheckState = false;

            char letter = char.ToLower(input);

            if (currentWord.Contains(letter))
            {
                char[] letters = currentWord.ToCharArray();
                char[] displayWord = WordUI.text.ToCharArray();

                for (int i = 0; i < letters.Length; i++)
                {
                    if (letters[i] == letter)
                    {
                        displayWord[i] = letter;
                    }
                }
                WordUI.text = string.Empty;
                foreach (char ch in displayWord)
                    WordUI.text += ch;
                SFXManager.Instance.PlaySFX(CorrectInput);
            }
            else
            {
                livesRemaining -= 1;
                score -= 10;
                SFXManager.Instance.PlaySFX(IncorrectInput);
                UpdateUI();
            }

            CheckPlayerState();
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            string s = Input.inputString;
            s = s.ToLower();
            char c;

            try
            {
                char[] cArray = s.ToCharArray();
                c = cArray[0];
            }
            catch
            {
                c = ' ';
            }

            if (!usedKeys.Contains(s))
            {
                usedKeys.Add(s);
                GUI.DisableButton(s);

                switch (c)
                {
                    case 'a':
                        ProcessInput(c);
                        break;
                    case 'b':
                        ProcessInput(c);
                        break;
                    case 'c':
                        ProcessInput(c);
                        break;
                    case 'd':
                        ProcessInput(c);
                        break;
                    case 'e':
                        ProcessInput(c);
                        break;
                    case 'f':
                        ProcessInput(c);
                        break;
                    case 'g':
                        ProcessInput(c);
                        break;
                    case 'h':
                        ProcessInput(c);
                        break;
                    case 'i':
                        ProcessInput(c);
                        break;
                    case 'j':
                        ProcessInput(c);
                        break;
                    case 'k':
                        ProcessInput(c);
                        break;
                    case 'l':
                        ProcessInput(c);
                        break;
                    case 'm':
                        ProcessInput(c);
                        break;
                    case 'n':
                        ProcessInput(c);
                        break;
                    case 'o':
                        ProcessInput(c);
                        break;
                    case 'p':
                        ProcessInput(c);
                        break;
                    case 'q':
                        ProcessInput(c);
                        break;
                    case 'r':
                        ProcessInput(c);
                        break;
                    case 's':
                        ProcessInput(c);
                        break;
                    case 't':
                        ProcessInput(c);
                        break;
                    case 'u':
                        ProcessInput(c);
                        break;
                    case 'v':
                        ProcessInput(c);
                        break;
                    case 'w':
                        ProcessInput(c);
                        break;
                    case 'x':
                        ProcessInput(c);
                        break;
                    case 'y':
                        ProcessInput(c);
                        break;
                    case 'z':
                        ProcessInput(c);
                        break;
                    default:
                        //invalid input
                        break;
                }
            }
        }
    }

    void CheckPlayerState()
    {
        if (livesRemaining <= 0)
        {
            if (score > 0)
                HighScores.AddToList(score);

            if (LostGame)
                SFXManager.Instance.PlaySFX(LostGame);

            CurrentWord = currentWord;
            SceneManager.LoadSceneAsync("FinishedWord", LoadSceneMode.Additive);
        }
        else if (!WordUI.text.Contains("_"))
        {
            if (availableWords.Count <= 0)
                WonGame();
            else
            {
                if (CorrectAnswer)
                    SFXManager.Instance.PlaySFX(CorrectAnswer);

                CurrentWord = currentWord;
                SceneManager.LoadSceneAsync("FinishedWord", LoadSceneMode.Additive);

                Invoke("Reset", 2.9f);
            }
        }
        else
        {
            canCheckState = true;
        }
    }

    void WonGame()
    {
        HighScores.AddToList(score);

        if (CorrectAnswer)
            SFXManager.Instance.PlaySFX(WinGame);

        SceneManager.LoadSceneAsync("FinishedGame", LoadSceneMode.Additive);
    }
}