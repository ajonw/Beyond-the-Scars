using System.Collections;
using Cinemachine;
using UnityEngine;

public class IndoorHomeFirstCutscene : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject mother;
    [SerializeField] public Transform playerTransform; // Assign the player transform in the Inspector
    [SerializeField] public Transform motherTransform;
    [SerializeField] public Animator playerAnimator;
    [SerializeField] public Animator motherAnimator;
    [SerializeField] public SO_Dialogue[] dialogues;

    [SerializeField] public Transform[] thingsToLookAt;

    [SerializeField] public CinemachineVirtualCamera virtualCamera;


    private DialogueManager dialogueManager;
    private Player_Controller pc;
    private float _moveSpeed = 3f;


    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        pc = player.GetComponent<Player_Controller>();
        playerTransform = player.transform;
        playerAnimator = player.GetComponent<Animator>();
        motherTransform = mother.transform;
        motherAnimator = mother.GetComponent<Animator>();
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        //Start character animation;
        pc.enabled = false;

        // Camera focus at player
        virtualCamera.Follow = playerTransform;

        // Player look up
        playerAnimator.SetBool("isMoving", true);
        playerAnimator.SetFloat("X", 0);
        playerAnimator.SetFloat("Y", 1);
        _moveSpeed = 2f;
        yield return StartCoroutine(MovePlayerToTarget(new Vector3(playerTransform.position.x, -7)));

        //Start dialogue
        yield return StartCoroutine(StartDialogue());

        yield return new WaitForSeconds(1f);



        // Camera focus at house
        virtualCamera.Follow = thingsToLookAt[0];
        yield return new WaitForSeconds(1.5f);

        //camera focus at player
        virtualCamera.Follow = playerTransform;
        yield return new WaitForSeconds(1f);

        //Player move up towards house
        playerAnimator.SetBool("isMoving", true);
        playerAnimator.SetFloat("X", 0);
        playerAnimator.SetFloat("Y", 1);
        yield return StartCoroutine(MovePlayerToTarget(new Vector3(playerTransform.position.x, -4.5f)));

        playerAnimator.SetBool("isMoving", false);
        pc.enabled = true;


        yield return new WaitForSeconds(10f);
    }

    private IEnumerator MovePlayerToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(playerTransform.position, targetPosition) > 0.1f)
        {
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPosition, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        playerTransform.position = targetPosition; // Ensure the player reaches the exact position
        yield return null;
    }

    private IEnumerator StartDialogue()
    {
        dialogueManager.InitiateDialogueForCutscene(dialogues[0]);
        while (dialogueManager.dialogueCompleted())
            yield return null;
    }
}

