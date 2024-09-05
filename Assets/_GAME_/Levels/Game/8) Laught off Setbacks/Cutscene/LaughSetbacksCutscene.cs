using System.Collections;
using Cinemachine;
using UnityEngine;

public class LaughSetbacksCutscene : MonoBehaviour
{
    [SerializeField] public GameObject adultPlayer;
    [SerializeField] public GameObject companion;
    private Transform adultTransform;
    private Transform companionTransform;
    private Animator adultAnimator;
    private Animator companionAnimator;

    [SerializeField] public SO_Dialogue[] dialogues;

    [SerializeField] public Transform[] thingsToLookAt;

    [SerializeField] public CinemachineVirtualCamera virtualCamera;


    [SerializeField] public DialogueManager dialogueManager;
    [SerializeField] public GameObject vaseImage;

    private float _moveSpeed = 3f;
    private Player_Controller _pc;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        adultTransform = adultPlayer.transform;
        adultAnimator = adultPlayer.GetComponent<Animator>();
        companionTransform = companion.transform;
        companionAnimator = companion.GetComponent<Animator>();
        _pc = adultPlayer.GetComponent<Player_Controller>();
        StartCoroutine(PlayCutscene());
        vaseImage.SetActive(false);
    }

    private IEnumerator PlayCutscene()
    {
        _pc.enabled = false;
        yield return new WaitForSeconds(11.5f);
        LookRight(adultAnimator);
        LookLeft(companionAnimator);
        yield return new WaitForSeconds(0.4f);
        yield return StartCoroutine(StartDialogue(0));
        yield return new WaitForSeconds(0.5f);
        vaseImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(StartDialogue(1));
        yield return new WaitForSeconds(0.5f);
        vaseImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(StartDialogue(1));
        LookDown(adultAnimator);
        LookDown(companionAnimator);
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
