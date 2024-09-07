using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EmbraceChildCutscene : MonoBehaviour
{
    [SerializeField] public GameObject adultChildObject;
    [SerializeField] public SO_Dialogue[] dialogues;
    [SerializeField] public Transform[] thingsToLookAt;
    [SerializeField] public CinemachineVirtualCamera virtualCamera;

    private Transform adultChildTransform;
    private Animator adultChildAnimator;
    private DialogueManager dialogueManager;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        adultChildTransform = adultChildObject.transform;
        adultChildAnimator = adultChildObject.GetComponent<Animator>();
        StartCoroutine(PlayCutscene());
        HappyActivityCompletionData.completedEmbrace = true;
        HappyActivityCompletionData.firstTime = false;
        HappyActivityCompletionData.xVal = 1.89f;
        HappyActivityCompletionData.yVal = 6.69f;
    }

    private IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(1f);
        //Start dialogue
        yield return StartCoroutine(StartDialogue(0));
        yield return new WaitForSeconds(0.5f);
        adultChildAnimator.SetBool("isHugging", true);
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(StartDialogue(1));
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator StartDialogue(int index)
    {
        dialogueManager.InitiateDialogueForCutscene(dialogues[index]);
        while (dialogueManager.dialogueCompleted())
            yield return null;
    }
}
