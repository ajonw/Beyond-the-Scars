using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CutsceneOutsideSchool : MonoBehaviour
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

        // Camera focus at companion
        virtualCamera.Follow = playerTransform;

        yield return new WaitForSeconds(1.5f);

        //Start Companion Intro dialogue
        yield return StartCoroutine(StartDialogue(0));

        yield return new WaitForSeconds(1f);

        //player move up left
        LookLeft(playerAnimator);
        StartMoving(playerAnimator);
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(13.21f, -8.94f)));
        //player move left
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(5.04f, playerTransform.position.y)));
        //player move up
        LookUp(playerAnimator);
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(playerTransform.position.x, -4.64f)));
        StopMoving(playerAnimator);

        LookDown(playerAnimator);


        //Second dialogue
        yield return StartCoroutine(StartDialogue(1));

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
