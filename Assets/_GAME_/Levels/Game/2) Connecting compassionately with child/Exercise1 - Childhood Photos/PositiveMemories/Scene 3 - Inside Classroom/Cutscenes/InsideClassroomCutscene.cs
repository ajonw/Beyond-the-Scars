using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class InsideClassroomCutscene : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject teacher;
    [SerializeField] public GameObject friend1;
    [SerializeField] public GameObject friend2;
    [SerializeField] public SO_Dialogue[] dialogues;
    [SerializeField] public Transform[] thingsToLookAt;
    [SerializeField] public CinemachineVirtualCamera virtualCamera;
    [SerializeField] public DrawingGameManager gameManager;

    private Transform playerTransform;
    private Animator playerAnimator;

    private Transform teacherTransform;
    private Animator teacherAnimator;

    private Transform friend1Transform;
    private Animator friend1Animator;

    private Transform friend2Transform;
    private Animator friend2Animator;

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

        teacherTransform = teacher.transform;
        teacherAnimator = teacher.GetComponent<Animator>();

        friend1Transform = friend1.transform;
        friend1Animator = friend1.GetComponent<Animator>();

        friend2Transform = friend2.transform;
        friend2Animator = friend2.GetComponent<Animator>();

        StartCoroutine(PlayCutscene());
        PositiveMemoriesCompletionData.xVal = -1.53f;
        PositiveMemoriesCompletionData.yVal = -10.53f;
        PositiveMemoriesCompletionData.completedArt = true;
        PositiveMemoriesCompletionData.firstTime = false;
    }

    private IEnumerator PlayCutscene()
    {
        //Start character animation;
        pc.enabled = false;

        // Camera focus at companion
        virtualCamera.Follow = playerTransform;

        LookRight(friend2Animator);
        LookLeft(friend1Animator);

        yield return new WaitForSeconds(1f);

        //Player walks up
        _moveSpeed = 2f;
        LookUp(playerAnimator);
        StartMoving(playerAnimator);
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(playerTransform.position.x, -9.7f)));
        // Player walks right
        LookRight(playerAnimator);
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(3f, playerTransform.position.y)));
        // Player walks up right
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(4.68f, -8.24f)));
        StopMoving(playerAnimator);
        LookUp(playerAnimator);
        LookDown(friend1Animator);
        LookDown(friend2Animator);

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(StartDialogue(0));

        yield return new WaitForSeconds(1f);

        //friend 2 walks left 
        LookLeft(friend2Animator);
        StartMoving(friend2Animator);
        yield return StartCoroutine(MoveTransformToTarget(friend2Transform, new Vector3(-1.17f, friend2Transform.position.y)));
        //friend 2 waks up
        LookUp(friend2Animator);
        yield return StartCoroutine(MoveTransformToTarget(friend2Transform, new Vector3(friend2Transform.position.x, -6.42f)));
        //friend 2 sits
        StopMoving(friend2Animator);
        friend2Animator.SetBool("isSitting", true);

        //friend 1 walks left 
        LookLeft(friend1Animator);
        StartMoving(friend1Animator);
        yield return StartCoroutine(MoveTransformToTarget(friend1Transform, new Vector3(2.06f, friend1Transform.position.y)));
        //friend 1 waks up
        LookUp(friend1Animator);
        yield return StartCoroutine(MoveTransformToTarget(friend1Transform, new Vector3(friend1Transform.position.x, -6.42f)));
        //friend 1 sits
        StopMoving(friend1Animator);
        friend1Animator.SetBool("isSitting", true);

        //player walks left 
        LookLeft(playerAnimator);
        StartMoving(playerAnimator);
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(3.16f, -8.938f)));
        yield return StartCoroutine(MoveTransformToTarget(playerTransform, new Vector3(2.053f, playerTransform.position.y)));
        LookUp(playerAnimator);
        //player sits
        StopMoving(playerAnimator);
        playerAnimator.SetBool("isSitting", true);

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(StartDialogue(1));

        virtualCamera.Follow = playerTransform;
        gameManager.StartGame();
        // pc.enabled = true;
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
