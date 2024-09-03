using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class InsideSchoolCutscene : MonoBehaviour
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

        if (PositiveMemoriesCompletionData.firstTime)
        {
            StartCoroutine(PlayCutscene());
        }
        else
        {
            playerTransform.position = new Vector3(PositiveMemoriesCompletionData.xVal, PositiveMemoriesCompletionData.yVal);
        }

        // Both activites completed
        if (PositiveMemoriesCompletionData.completedArt && PositiveMemoriesCompletionData.completedBasketball)
        {
            CompletionData.firstTime = false;
            CompletionData.completedHappyMemory = true;
            StartCoroutine(PlayEnding());
        }
        PositiveMemoriesCompletionData.firstTime = false;
    }

    private IEnumerator PlayEnding()
    {
        pc.enabled = false;
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(StartDialogue(1));
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator PlayCutscene()
    {
        //Start character animation;
        pc.enabled = false;

        // Camera focus at companion
        virtualCamera.Follow = playerTransform;

        yield return new WaitForSeconds(0.5f);

        //player move up 
        LookUp(playerAnimator);
        StartMoving(playerAnimator);
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(playerTransform.position.x, -12.5f)));
        StopMoving(playerAnimator);
        LookDown(playerAnimator);
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(StartDialogue(0));

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
