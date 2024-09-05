using System.Collections;
using UnityEngine;

public class EndingCutscene : MonoBehaviour
{
    [SerializeField] public GameObject companion;
    [SerializeField] public SO_Dialogue[] dialogues;

    [SerializeField] public DialogueManager dialogueManager;
    [SerializeField] public GameObject replayCanvas;

    private float _moveSpeed = 3f;
    private Transform companionTransform;

    private Animator companionAnimator;




    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        companionTransform = companion.transform;
        companionAnimator = companion.GetComponent<Animator>();
        replayCanvas.SetActive(false);
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(11.5f);
        yield return StartCoroutine(StartDialogue(0));
        yield return new WaitForSeconds(1f);
        if (RewardSystem.secureAttachmentLevel == 3)
        {
            // No Mistakes made
            yield return StartCoroutine(StartDialogue(1));
        }
        else
        {
            // mistakes
            yield return StartCoroutine(StartDialogue(2));
        }
        yield return new WaitForSeconds(0.5f);
        replayCanvas.SetActive(true);

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
