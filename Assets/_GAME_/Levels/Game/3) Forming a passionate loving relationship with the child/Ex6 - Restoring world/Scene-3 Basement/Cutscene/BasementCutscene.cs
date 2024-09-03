using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BasementCutscene : MonoBehaviour
{
    [SerializeField] public GameObject adultPlayer;
    [SerializeField] public GameObject childPlayer;
    [SerializeField] public GameObject companion;

    [SerializeField] public Transform centreScreen;
    [SerializeField] public GameObject interactInstruction;

    private Transform adultTransform;
    private Transform childTransform;
    private Transform companionTransform;
    private Animator adultAnimator;
    private Animator childAnimator;
    private Animator companionAnimator;

    [SerializeField] public SO_Dialogue[] dialogues;

    [SerializeField] public Transform[] thingsToLookAt;

    [SerializeField] public CinemachineVirtualCamera virtualCamera;


    private DialogueManager dialogueManager;
    private float _moveSpeed = 3f;
    private Player_Controller _pc;

    // Start is called before the first frame update
    void Start()
    {
        // dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        adultTransform = adultPlayer.transform;
        adultAnimator = adultPlayer.GetComponent<Animator>();
        childTransform = childPlayer.transform;
        childAnimator = childPlayer.GetComponent<Animator>();
        // companionTransform = companion.transform;
        // companionAnimator = companion.GetComponent<Animator>();
        _pc = adultPlayer.GetComponent<Player_Controller>();
        // interactInstruction.SetActive(false);

        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        childAnimator.SetBool("isCrying", true);
        _pc.enabled = false;
        yield return new WaitForSeconds(1f);
        virtualCamera.Follow = childTransform;
        yield return new WaitForSeconds(2f);
        virtualCamera.Follow = adultTransform;
        //Start dialogue
        yield return new WaitForSeconds(1f);
        _pc.enabled = true;
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
