using System.Collections;
using Cinemachine;
using UnityEngine;

public class IndoorHomeFirstCutscene : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject mother;
    [SerializeField] public SO_Dialogue[] dialogues;
    [SerializeField] public Transform[] thingsToLookAt;
    [SerializeField] public CinemachineVirtualCamera virtualCamera;

    private Transform playerTransform; // Assign the player transform in the Inspector
    private Transform motherTransform;
    private Animator playerAnimator;
    private Animator motherAnimator;


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

        // Mother looks up at TV
        motherAnimator.SetBool("isMoving", false);
        motherAnimator.SetFloat("X", 0);
        motherAnimator.SetFloat("Y", 1);

        // Player walks up
        playerAnimator.SetBool("isMoving", true);
        playerAnimator.SetFloat("X", 0);
        playerAnimator.SetFloat("Y", 1);
        _moveSpeed = 2f;
        yield return StartCoroutine(MovePlayerToTarget(new Vector3(playerTransform.position.x, -7)));

        //Player looks right
        playerAnimator.SetBool("isMoving", false);
        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetFloat("X", 1);
        playerAnimator.SetFloat("Y", 0);

        yield return new WaitForSeconds(1.5f);

        //Start dialogue
        yield return StartCoroutine(StartDialogue());

        yield return new WaitForSeconds(1f);

        // Player looks up
        playerAnimator.SetFloat("X", 0);
        playerAnimator.SetFloat("Y", 1);


        pc.enabled = true;
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

