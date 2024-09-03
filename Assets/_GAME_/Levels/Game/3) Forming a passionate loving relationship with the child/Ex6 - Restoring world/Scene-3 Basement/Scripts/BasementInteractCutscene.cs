using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BasementInteractCutscene : MonoBehaviour
{
    [SerializeField] public GameObject adultPlayer;
    [SerializeField] public GameObject childPlayer;
    [SerializeField] public GameObject stairsTransition;
    [SerializeField] public NPCFollow npcFollow;
    private Transform adultTransform;
    private Transform childTransform;
    private Animator adultAnimator;
    private Animator childAnimator;

    [SerializeField] public SO_Dialogue[] dialogues;

    [SerializeField] public Transform[] thingsToLookAt;

    [SerializeField] public CinemachineVirtualCamera virtualCamera;


    [SerializeField] public DialogueManager dialogueManager;
    private float _moveSpeed = 3f;
    private Player_Controller _pc;

    private bool cutsceneStarted = false;



    // Start is called before the first frame update
    void Start()
    {
        // dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        adultTransform = adultPlayer.transform;
        adultAnimator = adultPlayer.GetComponent<Animator>();
        childTransform = childPlayer.transform;
        childAnimator = childPlayer.GetComponent<Animator>();
        _pc = adultPlayer.GetComponent<Player_Controller>();
        stairsTransition.SetActive(false);
    }

    private IEnumerator PlayCutscene()
    {
        _pc.enabled = false;
        StopMoving(adultAnimator);
        LookRight(adultAnimator);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(StartDialogue(0));
        yield return new WaitForSeconds(1f);
        childAnimator.SetBool("isCrying", false);
        LookLeft(childAnimator);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(StartDialogue(1));
        yield return new WaitForSeconds(1f);

        // Start follow
        npcFollow.beginFollow = true;
        stairsTransition.SetActive(true);
        _pc.enabled = true;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !cutsceneStarted)
        {
            StartCoroutine(PlayCutscene());
            cutsceneStarted = true;
        }
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
