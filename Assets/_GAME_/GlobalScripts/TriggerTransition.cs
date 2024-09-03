using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerTransition : MonoBehaviour
{
    [SerializeField] public GameObject instruction;
    [SerializeField] public LevelLoader levelLoader;
    [SerializeField] public SceneField nextScene;
    private bool changeSceneEnabled = false;

    private bool transitionEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        if (instruction)
            instruction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (changeSceneEnabled && Input.GetKeyDown(KeyCode.Z) && transitionEnabled)
        {
            //Change scene to game
            levelLoader.LoadNextLevel(nextScene);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && transitionEnabled)
        {
            if (instruction)
                instruction.SetActive(true);
            changeSceneEnabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (instruction)
                instruction.SetActive(false);
            changeSceneEnabled = false;
        }
    }

    public void EnableTransition()
    {
        transitionEnabled = true;
    }

    public void DisableTransition()
    {
        transitionEnabled = false;
    }
}
