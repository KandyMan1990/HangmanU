using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    static SFXManager instance;
    AudioSource source;

    public static SFXManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (SFXManager)FindObjectOfType(typeof(SFXManager));
                if (instance == null)
                {
                    //Try loading a template prefab
                    GameObject templatePrefab = Resources.Load("SFXManager") as GameObject;

                    GameObject GM;
                    if (templatePrefab != null)
                    {
                        GM = Instantiate(templatePrefab);
                    }
                    else
                    {
                        //Create a new one in hierarchy, and this one will persist throughout the game/scene too.
                        GM = new GameObject("SFXManager");
                        GM.AddComponent<SFXManager>(); //This point Awake will be called
                    }

                    instance = GM.GetComponent<SFXManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        //Check for duplicates in the scene
        Object[] GMs = FindObjectsOfType(typeof(SFXManager));
        for (int i = 0; i < GMs.Length; i++)
        {
            if (GMs[i] != this)
            { //Not conform to singleton pattern
              //Self destruct!
                Destroy(gameObject);
            }
        }

        source = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public bool IsPlaying
    {
        get { return source.isPlaying; }
    }
}