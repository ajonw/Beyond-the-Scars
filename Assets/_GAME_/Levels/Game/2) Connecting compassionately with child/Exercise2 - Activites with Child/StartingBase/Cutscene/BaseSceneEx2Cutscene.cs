using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class BaseSceneEx2Cutscene : MonoBehaviour
{
    [SerializeField] Transform centreScreen;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject companion;

    [SerializeField] public SO_Dialogue[] dialogues;
    [SerializeField] public Transform[] thingsToLookAt;
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] public GameObject openPhotoScreenItem;
    [SerializeField] public PlayableDirector playableDirector;
    [SerializeField] public NPCDialogue companionDialogueHandler;

    private Transform playerTransform;
    private Transform companionTransform;
    private Animator playerAnimator;
    private Animator companionAnimator;

    private SpriteRenderer companionSpeechBubble;

    private DialogueManager dialogueManager;
    private Player_Controller pc;
    private float _moveSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();

        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        pc = player.GetComponent<Player_Controller>();
        playerTransform = player.transform;
        playerAnimator = player.GetComponent<Animator>();
        companionTransform = companion.transform;
        companionAnimator = companion.GetComponent<Animator>();
        companionSpeechBubble = GameObject.Find("DialogueHandler").GetComponent<SpriteRenderer>();


        companionSpeechBubble.enabled = false;

        openPhotoScreenItem.SetActive(false);

        if (Ex2CompletionData.firstTime)
        {
            StartCoroutine(PlayCutscene());
        }
        else
        {
            playableDirector.enabled = false;
            openPhotoScreenItem.SetActive(true);
        }
        Ex2CompletionData.firstTime = false;

        if (Ex2CompletionData.completedHappyActivity && Ex2CompletionData.completedUnhappyActivity)
        {
            StartCoroutine(PlayEnding());
        }
    }

    private IEnumerator PlayEnding()
    {
        pc.enabled = false;
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(StartDialogue(1));
    }

    private IEnumerator PlayCutscene()
    {
        //Start character animation;
        pc.enabled = false;

        // Camera focus at centre
        virtualCamera.Follow = centreScreen;

        yield return new WaitForSeconds(5f);
        openPhotoScreenItem.SetActive(true);

        //Companion looks right
        companionAnimator.SetFloat("X", 1);
        companionAnimator.SetFloat("Y", 0);


        //Player looks left
        playerAnimator.SetFloat("X", -1);
        playerAnimator.SetFloat("Y", 0);

        // Start dialouge 1
        yield return StartCoroutine(StartDialogue(0));

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
