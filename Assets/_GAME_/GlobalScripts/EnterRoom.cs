using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnterRoom : MonoBehaviour
{
    private Transform player;
    [SerializeField] public Sprite doorOpened;
    [SerializeField] public Sprite doorClosed;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] public string targetScene;

    private PlayerInput playerInput;

    private AudioSource audioSource;

    private bool inRange;

    private SpriteRenderer currentDoor;
    private bool changingScene;


    private void Start()
    {
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        currentDoor = GetComponent<SpriteRenderer>();
        currentDoor.sprite = doorClosed;
        inRange = false;
        changingScene = false;
        audioSource = GameObject.Find("DoorSound").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.Z) && !changingScene)
        {
            changingScene = true;
            levelLoader.LoadNextLevel(targetScene);
            audioSource.Play();
            playerInput.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // In proximity to door, change sprite to opened door
            currentDoor.sprite = doorOpened;
            player = collision.gameObject.GetComponent<Transform>();

            // If in proximity, check if button pressed if yes, change scene and play door sound
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Player leaves proximity door closes sprite
            currentDoor.sprite = doorClosed;
            inRange = false;
        }
    }
}