using UnityEngine;

public class DDOL : MonoBehaviour
{
    static DDOL instance;

    public static DDOL Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (DDOL)FindObjectOfType(typeof(DDOL));
                if (instance == null)
                {
                    //Try loading a template prefab
                    GameObject templatePrefab = Resources.Load("MusicManager") as GameObject;

                    GameObject GM;
                    if (templatePrefab != null)
                    {
                        GM = Instantiate(templatePrefab);
                    }
                    else
                    {
                        //Create a new one in hierarchy, and this one will persist throughout the game/scene too.
                        GM = new GameObject("Game Manager");
                        GM.AddComponent<DDOL>(); //This point Awake will be called
                    }

                    instance = GM.GetComponent<DDOL>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        //Check for duplicates in the scene
        Object[] GMs = FindObjectsOfType(typeof(DDOL));
        for (int i = 0; i < GMs.Length; i++)
        {
            if (GMs[i] != this)
            { //Not conform to singleton pattern
              //Self destruct!
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}