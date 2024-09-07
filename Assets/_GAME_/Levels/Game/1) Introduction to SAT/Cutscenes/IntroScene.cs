using System.Collections;
using Cinemachine;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    [SerializeField] Transform centreScreen;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject companion;
    [SerializeField] public GameObject childCharacter;
    [SerializeField] public SO_Dialogue[] dialogues;
    [SerializeField] public Transform[] thingsToLookAt;
    [SerializeField] public CinemachineVirtualCamera virtualCamera;

    private Transform playerTransform;
    private Transform companionTransform;
    private Transform childTransform;
    private Animator playerAnimator;
    private Animator companionAnimator;

    private SpriteRenderer companionSpeechBubble;
    private Animator childAnimator;

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
        companionTransform = companion.transform;
        companionAnimator = companion.GetComponent<Animator>();
        companionSpeechBubble = GameObject.Find("DialogueHandler").GetComponent<SpriteRenderer>();
        childTransform = childCharacter.transform;
        childAnimator = childCharacter.GetComponent<Animator>();

        childCharacter.SetActive(false);
        companionSpeechBubble.enabled = false;

        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        //Start character animation;
        pc.enabled = false;

        // Camera focus at centre
        virtualCamera.Follow = centreScreen;

        yield return new WaitForSeconds(11.5f);

        // Camera focus at companion
        virtualCamera.Follow = companionTransform;

        yield return new WaitForSeconds(1.5f);

        //Companion walks right
        companionAnimator.SetBool("isMoving", true);
        companionAnimator.SetFloat("X", 1);
        companionAnimator.SetFloat("Y", 0);
        _moveSpeed = 2f;
        yield return StartCoroutine(MoveTransformToTarget(companionTransform, new Vector3(-1, companionTransform.position.y)));
        companionAnimator.SetBool("isMoving", false);

        yield return new WaitForSeconds(1f);
        // Companion looks down
        companionAnimator.SetFloat("X", 0);
        companionAnimator.SetFloat("Y", -1);
        yield return new WaitForSeconds(1f);

        companionSpeechBubble.enabled = true;

        yield return new WaitForSeconds(1f);

        virtualCamera.Follow = playerTransform;

        yield return new WaitForSeconds(1f);
        // Player walks left
        playerAnimator.SetBool("isMoving", true);
        playerAnimator.SetFloat("X", -1);
        playerAnimator.SetFloat("Y", 0);
        _moveSpeed = 3f;
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(0.8f, playerTransform.position.y)));

        playerAnimator.SetBool("isMoving", false);
        yield return new WaitForSeconds(1.5f);
        companionAnimator.SetFloat("X", 1);
        companionAnimator.SetFloat("Y", 0);
        //Start dialogue
        companionSpeechBubble.enabled = false;
        yield return StartCoroutine(StartDialogue(0));

        yield return new WaitForSeconds(1.5f);

        //Child player appears
        childCharacter.SetActive(true);
        virtualCamera.Follow = childTransform;
        yield return new WaitForSeconds(1f);

        childAnimator.SetBool("isMoving", true);
        childAnimator.SetFloat("X", 0);
        childAnimator.SetFloat("Y", -1);
        yield return StartCoroutine(MoveTransformToTarget(childTransform, new Vector3(childTransform.position.x, 0)));
        childAnimator.SetBool("isMoving", false);

        yield return new WaitForSeconds(1f);

        //Second dialogue
        yield return StartCoroutine(StartDialogue(1));

        pc.enabled = true;
    }

    private IEnumerator MoveTransformToTarget(Transform transform, Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition; // Ensure the player reaches the exact position
        yield return null;
    }

    private IEnumerator StartDialogue(int index)
    {
        dialogueManager.InitiateDialogueForCutscene(dialogues[index]);
        while (dialogueManager.dialogueCompleted())
            yield return null;
    }
}
