using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // Destroys backgroundMusic
        GameObject backgroundMusic = GameObject.Find("BackgroundMusic");
        if (backgroundMusic)
        {
            Destroy(backgroundMusic);
            backgroundMusic = null;
            BackgroundMusic.backgroundMusic = null;
        }
    }
}
