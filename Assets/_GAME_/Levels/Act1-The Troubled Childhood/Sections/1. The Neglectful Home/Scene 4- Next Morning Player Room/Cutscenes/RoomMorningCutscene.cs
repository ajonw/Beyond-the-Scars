using System.Collections;
using Cinemachine;
using UnityEngine;

public class RoomMorningCutscene : MonoBehaviour
{
    [SerializeField] public GameObject player;

    [SerializeField] public SO_Dialogue[] dialogues;
    [SerializeField] public Transform[] thingsToLookAt;
    [SerializeField] public CinemachineVirtualCamera virtualCamera;

    private Transform playerTransform;
    private Animator playerAnimator;


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
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        //Start character animation;
        pc.enabled = false;

        //Player sleeping
        playerAnimator.SetBool("isSleeping", true);

        yield return new WaitForSeconds(1.5f);

        //Start dialogue
        yield return StartCoroutine(StartDialogue(0));
        yield return new WaitForSeconds(1f);

        playerAnimator.SetBool("isSleeping", false);

        // move 7.5
        playerAnimator.SetBool("isMoving", true);
        playerAnimator.SetFloat("X", 1);
        playerAnimator.SetFloat("Y", 0);
        _moveSpeed = 2f;
        yield return StartCoroutine(MovePlayerToTarget(new Vector3(-7.5f, playerTransform.position.y)));

        // Look down
        playerAnimator.SetBool("isMoving", false);
        playerAnimator.SetFloat("X", 0);
        playerAnimator.SetFloat("Y", -1);
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(StartDialogue(1));

        yield return new WaitForSeconds(1f);

        // move 7.5
        playerAnimator.SetBool("isMoving", true);
        playerAnimator.SetFloat("X", 0);
        playerAnimator.SetFloat("Y", -1);
        _moveSpeed = 2f;
        yield return StartCoroutine(MovePlayerToTarget(new Vector3(playerTransform.position.x, -2f)));

        yield return new WaitForSeconds(1f);

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

    private IEnumerator StartDialogue(int id)
    {
        dialogueManager.InitiateDialogueForCutscene(dialogues[id]);
        while (dialogueManager.dialogueCompleted())
            yield return null;
    }


}
