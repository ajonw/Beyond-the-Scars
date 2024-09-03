using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ComfortingChildCutscene : MonoBehaviour
{
    [SerializeField] public GameObject child;
    [SerializeField] public GameObject adultPlayer;
    [SerializeField] public GameObject companion;
    [SerializeField] public SO_Dialogue[] dialogues;
    [SerializeField] public Transform[] thingsToLookAt;
    [SerializeField] public CinemachineVirtualCamera virtualCamera;
    [SerializeField] GameObject slider;

    private Transform childTransform;
    private Animator childAnimator;

    private Animator playerAnimator;

    private Transform playerTransform;

    private Transform companionTransform;
    private Animator companionAnimator;

    private SpriteRenderer companionSpeechBubble;

    private DialogueManager dialogueManager;
    private Player_Controller pc;
    private float _moveSpeed = 2f;


    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        childTransform = child.transform;
        childAnimator = child.GetComponent<Animator>();
        pc = adultPlayer.GetComponent<Player_Controller>();
        playerTransform = adultPlayer.transform;
        playerAnimator = adultPlayer.GetComponent<Animator>();
        companionAnimator = companion.GetComponent<Animator>();
        companionTransform = companion.transform;
        companionSpeechBubble = GameObject.Find("DialogueHandler").GetComponent<SpriteRenderer>();

        slider.SetActive(false);
        pc.enabled = false;
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        //Player sleeping
        childAnimator.SetBool("isCrying", true);
        yield return new WaitForSeconds(5f);

        // Player walks left to 
        playerAnimator.SetBool("isMoving", true);
        playerAnimator.SetFloat("X", -1);
        playerAnimator.SetFloat("Y", 0);
        _moveSpeed = 2f;
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(-7.3f, playerTransform.position.y)));
        playerAnimator.SetBool("isMoving", false);
        yield return new WaitForSeconds(0.5f);

        //Companion looks right
        companionAnimator.SetFloat("X", 1);
        companionAnimator.SetFloat("Y", 0);
        yield return new WaitForSeconds(1f);
        //Start dialogue
        // companionSpeechBubble.enabled = false;
        yield return StartCoroutine(StartDialogue(0));

        yield return new WaitForSeconds(1f);

        companionAnimator.SetFloat("X", 0);
        companionAnimator.SetFloat("Y", -1);


        yield return new WaitForSeconds(1f);


        pc.enabled = true;
        yield return new WaitForSeconds(1f);
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
