using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SAT1Cutscene : MonoBehaviour
{
    [SerializeField] public GameObject child;

    [SerializeField] public SO_Dialogue[] dialogues;
    [SerializeField] public Transform[] thingsToLookAt;
    [SerializeField] public CinemachineVirtualCamera virtualCamera;

    private Transform childTransform;
    private Animator childAnimator;


    private DialogueManager dialogueManager;
    private Player_Controller pc;
    private float _moveSpeed = 3f;


    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        childTransform = child.transform;
        childAnimator = child.GetComponent<Animator>();
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        //Player sleeping
        childAnimator.SetBool("isCrying", true);

        yield return new WaitForSeconds(1f);
    }

    private IEnumerator MoveChildToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(childTransform.position, targetPosition) > 0.1f)
        {
            childTransform.position = Vector3.MoveTowards(childTransform.position, targetPosition, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        childTransform.position = targetPosition; // Ensure the player reaches the exact position
        yield return null;
    }

    private IEnumerator StartDialogue()
    {
        dialogueManager.InitiateDialogueForCutscene(dialogues[0]);
        while (dialogueManager.dialogueCompleted())
            yield return null;
    }


}
