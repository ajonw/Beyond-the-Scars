using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayChildCutscene : MonoBehaviour
{
    [SerializeField] public GameObject adultPlayer;
    [SerializeField] public GameObject childPlayer;

    [SerializeField] public RPSManager gameManager;
    private Transform adultTransform;
    private Transform childTransform;
    private Animator adultAnimator;
    private Animator childAnimator;

    [SerializeField] public SO_Dialogue[] dialogues;

    [SerializeField] public Transform[] thingsToLookAt;

    [SerializeField] public CinemachineVirtualCamera virtualCamera;


    private DialogueManager dialogueManager;
    private float _moveSpeed = 3f;


    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        adultTransform = adultPlayer.transform;
        adultAnimator = adultPlayer.GetComponent<Animator>();
        childTransform = childPlayer.transform;
        childAnimator = childPlayer.GetComponent<Animator>();
        StartCoroutine(PlayCutscene());
        HappyActivityCompletionData.completedPlay = true;
        HappyActivityCompletionData.firstTime = false;
        HappyActivityCompletionData.xVal = 1.89f;
        HappyActivityCompletionData.yVal = 6.69f;
    }

    private IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(1f);

        LookRight(adultAnimator);
        LookLeft(childAnimator);
        //Start dialogue
        yield return StartCoroutine(StartDialogue(0));
        yield return new WaitForSeconds(0.5f);

        gameManager.StartGame();

        yield return new WaitForSeconds(1f);
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
