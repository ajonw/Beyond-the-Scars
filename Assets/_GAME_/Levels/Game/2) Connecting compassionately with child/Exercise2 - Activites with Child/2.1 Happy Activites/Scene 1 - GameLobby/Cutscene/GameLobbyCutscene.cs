using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class GameLobbyCutscene : MonoBehaviour
{
    [SerializeField] Transform centreScreen;
    [SerializeField] Transform jukeBox;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject companion;
    [SerializeField] public GameObject childCharacter;
    [SerializeField] public SO_Dialogue[] dialogues;
    [SerializeField] public Transform[] thingsToLookAt;
    [SerializeField] public CinemachineVirtualCamera virtualCamera;
    [SerializeField] public GameObject questlog;
    [SerializeField] public PlayableDirector playableDirector;

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

        companionSpeechBubble.enabled = false;
        questlog.SetActive(false);

        if (HappyActivityCompletionData.firstTime)
        {
            StartCoroutine(PlayCutscene());
        }
        else
        {
            playableDirector.enabled = false;
            playerTransform.position = new Vector3(HappyActivityCompletionData.xVal, HappyActivityCompletionData.yVal);
            questlog.SetActive(true);
        }
        HappyActivityCompletionData.firstTime = false;

        if (HappyActivityCompletionData.completedEmbrace && HappyActivityCompletionData.completedPlay && HappyActivityCompletionData.completedDance)
        {
            StartCoroutine(PlayEnding());
            Ex2CompletionData.firstTime = false;
            Ex2CompletionData.completedHappyActivity = true;
        }
    }

    private IEnumerator PlayEnding()
    {
        //Start character animation;
        pc.enabled = false;
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(StartDialogue(3));
    }


    private IEnumerator PlayCutscene()
    {
        //Start character animation;
        pc.enabled = false;

        // Camera focus at centre
        virtualCamera.Follow = centreScreen;

        yield return new WaitForSeconds(12f);
        questlog.SetActive(true);

        // Camera focus at companion
        virtualCamera.Follow = playerTransform;

        yield return new WaitForSeconds(1.5f);

        // Player walks diagonal right
        playerAnimator.SetBool("isMoving", true);
        playerAnimator.SetFloat("X", 1);
        playerAnimator.SetFloat("Y", 0);
        _moveSpeed = 3f;
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(-3.7f, -4)));

        //player walk right
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(1.6f, playerTransform.position.y)));
        playerAnimator.SetBool("isMoving", false);


        //Companion looks left
        companionAnimator.SetFloat("X", -1);
        companionAnimator.SetFloat("Y", 0);

        yield return new WaitForSeconds(1f);

        //Start dialogue
        yield return StartCoroutine(StartDialogue(0));

        // Look at child
        virtualCamera.Follow = childTransform;

        yield return new WaitForSeconds(2f);

        virtualCamera.Follow = companionTransform;

        yield return new WaitForSeconds(1f);

        //Second dialogue
        yield return StartCoroutine(StartDialogue(1));


        // Look at JukeBox
        virtualCamera.Follow = jukeBox;

        yield return new WaitForSeconds(2f);

        virtualCamera.Follow = companionTransform;

        yield return new WaitForSeconds(1f);

        //Second dialogue
        yield return StartCoroutine(StartDialogue(2));

        // Player looks down
        playerAnimator.SetFloat("X", 0);
        playerAnimator.SetFloat("Y", -1);

        // Companion looks down
        companionAnimator.SetFloat("X", 0);
        companionAnimator.SetFloat("Y", -1);
        yield return new WaitForSeconds(1f);

        virtualCamera.Follow = playerTransform;

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

    private void StartMoving(Animator animator)
    {
        animator.SetBool("isMoving", true);
    }

    private void StopMoving(Animator animator)
    {
        animator.SetBool("isMoving", false);
    }

    private void LookRight(Animator animator)
    {
        animator.SetFloat("X", 1);
        animator.SetFloat("Y", 0);
    }

    private void LookLeft(Animator animator)
    {
        animator.SetFloat("X", -1);
        animator.SetFloat("Y", 0);
    }
    private void LookUp(Animator animator)
    {
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", 1);
    }

    private void LookDown(Animator animator)
    {
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", -1);
    }

    private IEnumerator StartDialogue(int index)
    {
        dialogueManager.InitiateDialogueForCutscene(dialogues[index]);
        while (dialogueManager.dialogueCompleted())
            yield return null;
    }
}
