using UnityEngine;
using UnityEngine.InputSystem;

public class EnterDoor : MonoBehaviour
{
    [SerializeField] public Sprite doorOpened;
    [SerializeField] public Sprite doorClosed;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] public SceneField targetScene;
    [SerializeField] public GameObject instructionText;

    private PlayerInput playerInput;

    private AudioSource audioSource;

    private bool inRange;

    private SpriteRenderer currentDoor;
    private bool changingScene;

    private bool doorEnabled = true;


    private void Start()
    {
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        currentDoor = GetComponent<SpriteRenderer>();
        currentDoor.sprite = doorClosed;
        inRange = false;
        changingScene = false;
        audioSource = GameObject.Find("DoorSound").GetComponent<AudioSource>();
        if (instructionText)
            instructionText.SetActive(false);
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.Z) && !changingScene && doorEnabled)
        {
            changingScene = true;
            levelLoader.LoadNextLevel(targetScene);
            audioSource.Play();
            playerInput.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && doorEnabled)
        {
            // In proximity to door, change sprite to opened door
            currentDoor.sprite = doorOpened;

            // If in proximity, check if button pressed if yes, change scene and play door sound
            inRange = true;
            if (instructionText)
                instructionText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Player leaves proximity door closes sprite
            currentDoor.sprite = doorClosed;
            inRange = false;
            if (instructionText)
                instructionText.SetActive(false);
        }
    }

    public void EnableDoor()
    {
        doorEnabled = true;
    }

    public void DisableDoor()
    {
        doorEnabled = false;
    }
}