using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class IndoorHomeConflictCutscene : MonoBehaviour
{
    [SerializeField] public GameObject childPlayer;
    [SerializeField] public GameObject mother;

    [SerializeField] public GameObject father;
    private Transform childTransform;
    private Transform motherTransform;
    private Transform fatherrTransform;
    private Animator childAnimator;
    private Animator motherAnimator;
    private Animator fatherAnimator;

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
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        childTransform = childPlayer.transform;
        childAnimator = childPlayer.GetComponent<Animator>();
        motherTransform = mother.transform;
        motherAnimator = mother.GetComponent<Animator>();
        fatherrTransform = father.transform;
        fatherAnimator = father.GetComponent<Animator>();
        _pc = childPlayer.GetComponent<Player_Controller>();
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        _pc.enabled = false;
        yield return new WaitForSeconds(1f);
        LookLeft(childAnimator);
        StartMoving(childAnimator);
        yield return StartCoroutine(MoveTransformToTarget(childTransform, new Vector3(12.3f, 6.22f)));
        yield return StartCoroutine(MoveTransformToTarget(childTransform, new Vector3(9.3f, 6.22f)));
        yield return StartCoroutine(MoveTransformToTarget(childTransform, new Vector3(4.9f, 7.66f)));
        StopMoving(childAnimator);

        yield return new WaitForSeconds(0.5f);
        LookRight(motherAnimator);
        yield return StartCoroutine(StartDialogue(0));
        yield return new WaitForSeconds(0.5f);

        LookDown(motherAnimator);
        LookRight(childAnimator);
        StartMoving(childAnimator);
        yield return StartCoroutine(MoveTransformToTarget(childTransform, new Vector3(6.04f, 5.06f)));
        LookDown(childAnimator);
        yield return StartCoroutine(MoveTransformToTarget(childTransform, new Vector3(6.04f, -0.78f)));
        LookLeft(childAnimator);
        yield return StartCoroutine(MoveTransformToTarget(childTransform, new Vector3(0.58f, -1.32f)));
        yield return StartCoroutine(MoveTransformToTarget(childTransform, new Vector3(-2.56f, -5.39f)));
        StopMoving(childAnimator);

        yield return new WaitForSeconds(0.5f);
        LookRight(fatherAnimator);
        yield return StartCoroutine(StartDialogue(1));
        yield return new WaitForSeconds(0.5f);

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
