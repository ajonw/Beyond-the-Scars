using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ConsoleHugCutscene : MonoBehaviour
{
    [SerializeField] public GameObject adultChild;

    [SerializeField] public SO_Dialogue[] dialogues;

    [SerializeField] public Transform[] thingsToLookAt;

    [SerializeField] public CinemachineVirtualCamera virtualCamera;

    private Transform adultChildTransform;
    private Animator adultChildAnimation;
    private DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        adultChildTransform = adultChild.transform;
        adultChildAnimation = adultChild.GetComponent<Animator>();
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(StartDialogue(0));
        yield return new WaitForSeconds(0.5f);
        adultChildAnimation.SetBool("isHugging", true);
        yield return new WaitForSeconds(2f);
        //Start dialogue
        yield return StartCoroutine(StartDialogue(1));

        yield return new WaitForSeconds(10f);
    }

    private IEnumerator StartDialogue(int index)
    {
        dialogueManager.InitiateDialogueForCutscene(dialogues[index]);
        while (dialogueManager.dialogueCompleted())
            yield return null;
    }
}
