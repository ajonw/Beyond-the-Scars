using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1;

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         LoadNextLevel();
    //     }
    // }

    public void LoadNextLevel(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        // Play animation
        transition.SetTrigger("Start");

        // Wait animation to stop
        yield return new WaitForSeconds(transitionTime);

        // Load scene
        SceneManager.LoadScene(sceneName);
    }
}
