using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugSpouseCutscene : MonoBehaviour
{
    [SerializeField] public GameObject spouseHugObject;

    private Transform spouseHugTransform;

    private Animator spouseHugAnimator;

    [SerializeField] public SO_Dialogue[] dialogues;

    public DialogueManager dialogueManager;

    private float _moveSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        spouseHugTransform = spouseHugObject.transform;
        spouseHugAnimator = spouseHugObject.GetComponent<Animator>();

        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(StartDialogue(0));
        yield return new WaitForSeconds(0.2f);
        spouseHugAnimator.SetBool("isHugging", true);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(StartDialogue(1));
        yield return new WaitForSeconds(1f);
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
    private IEnumerator StartDialogue(int index)
    {
        dialogueManager.InitiateDialogueForCutscene(dialogues[index]);
        while (dialogueManager.dialogueCompleted())
            yield return null;
    }
}
