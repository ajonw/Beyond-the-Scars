using System.Collections;
using Cinemachine;
using UnityEngine;

public class AdultHugsChildCutscene : MonoBehaviour
{
    [SerializeField] public GameObject adultChild;
    private Transform adultChildTransform;
    private Animator adultChildAnimator;
    [SerializeField] public SO_Dialogue[] dialogues;

    [SerializeField] public Transform[] thingsToLookAt;

    [SerializeField] public CinemachineVirtualCamera virtualCamera;


    private DialogueManager dialogueManager;
    private Player_Controller pc;
    private float _moveSpeed = 3f;


    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        adultChildTransform = adultChild.transform;
        adultChildAnimator = adultChild.GetComponent<Animator>();
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        adultChildAnimator.SetBool("isHugging", true);
        yield return new WaitForSeconds(2f);
        //Start dialogue
        yield return StartCoroutine(StartDialogue());


        yield return new WaitForSeconds(10f);
    }

    private IEnumerator StartDialogue()
    {
        dialogueManager.InitiateDialogueForCutscene(dialogues[0]);
        while (dialogueManager.dialogueCompleted())
            yield return null;
    }
}
