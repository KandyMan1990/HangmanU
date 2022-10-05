using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [SerializeField] AudioClip CorrectInput;
    [SerializeField] AudioClip IncorrectInput;
    [SerializeField] AudioClip CorrectAnswer;
    [SerializeField] AudioClip LostGame;
    [SerializeField] AudioClip WinGame;
    [SerializeField] List<string> availableWords = new();

    int currentRoundScore;
    int score;
    string currentWord;
    readonly List<string> usedKeys = new();
    bool canCheckState = true;
    UpdateGUI GUI;
    List<string> words;
    ScoreType scoreType;
    bool gameInProgress = false;

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
    public int CurrentRoundScore
    {
        get { return currentRoundScore; }
    }
    public int Score
    {
        get { return score; }
    }
    public string CurrentWord { get; private set; }
    public ScoreType ActiveScoreType
    {
        get { return scoreType; }
    }
    public string DatabaseType
    {
        get { return scoreType == ScoreType.GAME ? "Games" : "Movies"; }
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

        DontDestroyOnLoad(gameObject);
    }
    
    public void StartGame()
    {
        availableWords.Clear();
        availableWords.TrimExcess();

        availableWords.AddRange(words);

        GUI = FindObjectOfType(typeof(UpdateGUI)) as UpdateGUI;
        score = 0;
        Reset();
        gameInProgress = true;
    }

    public void SetWords(WordDatabase db, ScoreType scoreType)
    {
        words = db.GetWords();
        this.scoreType = scoreType;
    }

    void Reset()
    {
        if (availableWords.Count > 0)
        {
            currentRoundScore = 100;

            int i = Random.Range(0, availableWords.Count);
            currentWord = availableWords[i];
            availableWords.RemoveAt(i);

            char[] characters = currentWord.ToCharArray();

            GUI.Reset(characters, currentRoundScore);

            canCheckState = true;
            GUI.EnableButtons(true);
            usedKeys.Clear();
            usedKeys.TrimExcess();
            GUI.SetCanvasActive(true);
            GUI.UpdateUI(currentRoundScore, score);
        }
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
                char[] displayWord = GUI.GetWordAsCharArray();

                for (int i = 0; i < letters.Length; i++)
                {
                    if (letters[i] == letter)
                    {
                        displayWord[i] = letter;
                    }
                }
                string wordUI_Text = string.Empty;

                foreach (char ch in displayWord)
                {
                    wordUI_Text += ch;
                }

                GUI.SetWordText(wordUI_Text);

                SFXManager.Instance.PlaySFX(CorrectInput);
            }
            else
            {
                currentRoundScore -= 10;
                SFXManager.Instance.PlaySFX(IncorrectInput);
                GUI.UpdateUI(currentRoundScore, score);
            }

            CheckPlayerState();
        }
    }

    void Update()
    {
        if (gameInProgress)
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
                        case 'b':
                        case 'c':
                        case 'd':
                        case 'e':
                        case 'f':
                        case 'g':
                        case 'h':
                        case 'i':
                        case 'j':
                        case 'k':
                        case 'l':
                        case 'm':
                        case 'n':
                        case 'o':
                        case 'p':
                        case 'q':
                        case 'r':
                        case 's':
                        case 't':
                        case 'u':
                        case 'v':
                        case 'w':
                        case 'x':
                        case 'y':
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
    }

    void CheckPlayerState()
    {
        if (currentRoundScore <= 0)
        {
            if (LostGame)
                SFXManager.Instance.PlaySFX(LostGame);

            CurrentWord = currentWord;

            EndGame("FinishedWord");
        }
        else if (!GUI.GetWord().Contains("_"))
        {
            if (availableWords.Count <= 0)
                WonGame();
            else
            {
                if (CorrectAnswer)
                    SFXManager.Instance.PlaySFX(CorrectAnswer);

                CurrentWord = currentWord;
                SceneManager.LoadSceneAsync("FinishedWord", LoadSceneMode.Additive);
                GUI.SetCanvasActive(false);
                score += currentRoundScore;
                Invoke("Reset", 3.1f);
            }
        }
        else
        {
            canCheckState = true;
        }
    }

    void WonGame()
    {
        score += currentRoundScore;

        if (CorrectAnswer)
            SFXManager.Instance.PlaySFX(CorrectAnswer);

        EndGame("FinishedGame");
    }

    void EndGame(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        GUI.SetCanvasActive(false);
        gameInProgress = false;
    }
}