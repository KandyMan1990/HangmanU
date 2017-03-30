using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{

    void Awake()
    {

        if(Application.platform == RuntimePlatform.WebGLPlayer || Application.isMobilePlatform)
        {
            gameObject.SetActive(false);
        }
    }
}