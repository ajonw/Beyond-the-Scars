using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnterStairs : MonoBehaviour
{
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] public SceneField targetScene;

    [SerializeField] public AudioSource audioSource;
    private bool changingScene;

    private PlayerInput playerInput;


    private void Start()
    {
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        changingScene = false;
        audioSource = GameObject.Find("StairsSound").GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !changingScene)
        {
            changingScene = true;
            levelLoader.LoadNextLevel(targetScene);
            audioSource.Play();
            playerInput.enabled = false;
        }
    }
}
