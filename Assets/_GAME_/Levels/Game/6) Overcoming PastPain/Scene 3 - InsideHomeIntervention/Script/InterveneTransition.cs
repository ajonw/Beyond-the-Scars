using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterveneTransition : MonoBehaviour
{
    public LevelLoader levelLoader;
    public SceneField nextScene;
    public GameObject instruction;
    private bool readyTransition = false;
    // Start is called before the first frame update
    void Start()
    {
        instruction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && readyTransition)
        {
            levelLoader.LoadNextLevel(nextScene);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            instruction.SetActive(true);
            readyTransition = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (instruction)
                instruction.SetActive(false);
            readyTransition = false;
        }
    }
}
