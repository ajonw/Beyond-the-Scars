using System.Collections;
using Cinemachine;
using UnityEngine;

public class ChildLovedCutscene : MonoBehaviour
{
    [SerializeField] public GameObject adultPlayer;
    [SerializeField] public GameObject spouse;
    [SerializeField] public GameObject daughter;
    private Transform adultTransform;
    private Transform spouseTransform;
    private Transform daughterTransform;
    private Animator adultAnimator;
    private Animator spouseAnimator;
    private Animator daughterAnimator;

    [SerializeField] public SO_Dialogue[] dialogues;

    [SerializeField] public CinemachineVirtualCamera virtualCamera;

    public DialogueManager dialogueManager;
    private float _moveSpeed = 3f;
    private Player_Controller _pc;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        adultTransform = adultPlayer.transform;
        adultAnimator = adultPlayer.GetComponent<Animator>();
        spouseTransform = spouse.transform;
        spouseAnimator = spouse.GetComponent<Animator>();
        daughterTransform = daughter.transform;
        daughterAnimator = daughter.GetComponent<Animator>();

        _pc = adultPlayer.GetComponent<Player_Controller>();
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        _pc.enabled = false;
        LookRight(adultAnimator);
        LookLeft(daughterAnimator);
        LookUp(spouseAnimator);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(StartDialogue(0));
        yield return new WaitForSeconds(0.5f);
        daughterAnimator.SetBool("isLoved", true);
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(StartDialogue(1));
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
