using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FinalUnhappyCutscene : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject child;

    [SerializeField] public SO_Dialogue[] dialogues;
    [SerializeField] public Transform[] thingsToLookAt;
    [SerializeField] public CinemachineVirtualCamera virtualCamera;

    private Transform playerTransform;
    private Transform childTransform;
    private Animator playerAnimator;
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
        childTransform = child.transform;
        childAnimator = child.GetComponent<Animator>();

        StartCoroutine(PlayCutscene());

        Ex2CompletionData.firstTime = false;
        Ex2CompletionData.completedUnhappyActivity = true;

    }

    private IEnumerator PlayCutscene()
    {
        //Start character animation;
        pc.enabled = false;
        childAnimator.SetBool("isCrying", false);
        yield return new WaitForSeconds(1f);
        LookRight(playerAnimator);
        LookLeft(childAnimator);
        //starting
        yield return StartCoroutine(StartDialogue(0));
        yield return new WaitForSeconds(1f);

        //Fearful
        childAnimator.SetBool("isFearful", true);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(StartDialogue(1));
        yield return new WaitForSeconds(1f);
        childAnimator.SetBool("isFearful", false);
        LookLeft(childAnimator);
        yield return StartCoroutine(StartDialogue(2));


        //Anger
        yield return new WaitForSeconds(1f);
        childAnimator.SetBool("isAngry", true);
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(StartDialogue(3));
        yield return new WaitForSeconds(0.5f);
        childAnimator.SetBool("isAngry", false);
        LookLeft(childAnimator);
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(StartDialogue(4));
        yield return new WaitForSeconds(0.5f);

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
