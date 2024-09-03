using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class UnhappyActivityCutscene : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject child;
    [SerializeField] public GameObject companion;
    [SerializeField] public SO_Dialogue[] dialogues;
    [SerializeField] public Transform[] thingsToLookAt;
    [SerializeField] public CinemachineVirtualCamera virtualCamera;

    [SerializeField] public PlayableDirector playableDirector;

    private Transform playerTransform;
    private Transform childTransform;
    private Transform companionTransform;
    private Animator playerAnimator;
    private Animator childAnimator;
    private Animator companionAnimator;

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
        companionTransform = companion.transform;
        companionAnimator = companion.GetComponent<Animator>();

        StartCoroutine(PlayCutscene());

    }

    private IEnumerator PlayCutscene()
    {
        //Start character animation;
        pc.enabled = false;
        yield return new WaitForSeconds(12f);
        childAnimator.SetBool("isCrying", true);
        // Camera focus at companion
        virtualCamera.Follow = playerTransform;

        //player move down
        LookDown(playerAnimator);
        StartMoving(playerAnimator);
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(playerTransform.position.x, -9.8f)));
        LookRight(playerAnimator);
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(-11.5f, -10.7f)));
        StopMoving(playerAnimator);
        yield return new WaitForSeconds(1f);
        LookLeft(companionAnimator);
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(StartDialogue(0));

        virtualCamera.Follow = childTransform;

        yield return new WaitForSeconds(2f);

        // virtualCamera.m_Lens.OrthographicSize = 4;

        virtualCamera.Follow = playerTransform;
        yield return new WaitForSeconds(1f);

        LookDown(playerAnimator);
        LookDown(companionAnimator);

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
