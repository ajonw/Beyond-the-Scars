using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class IndoorHomeInterventionCutscene : MonoBehaviour
{
    [SerializeField] public GameObject adultPlayer;
    [SerializeField] public GameObject companion;
    [SerializeField] public GameObject childPlayer;
    [SerializeField] public GameObject mother;
    [SerializeField] public GameObject father;

    private Transform adultTransform;
    private Transform companionTransform;
    private Transform childTransform;
    private Transform motherTransform;
    private Transform fatherTransform;

    private Animator adultAnimator;
    private Animator companionAnimator;
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


    //Camera Shake
    private float shakeIntensity = 1f;
    private float shakeTime = 0.5f;
    private float timer;
    CinemachineBasicMultiChannelPerlin _cbmcp;


    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        adultTransform = adultPlayer.transform;
        adultAnimator = adultPlayer.GetComponent<Animator>();
        companionTransform = companion.transform;
        companionAnimator = companion.GetComponent<Animator>();
        childTransform = childPlayer.transform;
        childAnimator = childPlayer.GetComponent<Animator>();
        motherTransform = mother.transform;
        motherAnimator = mother.GetComponent<Animator>();
        fatherTransform = father.transform;
        fatherAnimator = father.GetComponent<Animator>();
        _pc = adultPlayer.GetComponent<Player_Controller>();
        if (virtualCamera)
            _cbmcp = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (_cbmcp)
            StopShake();
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        _pc.enabled = false;
        childAnimator.SetBool("isFallen", true);
        LookRight(fatherAnimator);
        yield return new WaitForSeconds(1f);
        ShakeCamera();
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(StartDialogue(0));
        yield return new WaitForSeconds(2f);
        LookLeft(companionAnimator);
        LookRight(adultAnimator);
        yield return StartCoroutine(StartDialogue(1));
        yield return new WaitForSeconds(1f);
        virtualCamera.Follow = adultTransform;
        LookDown(companionAnimator);
        LookDown(adultAnimator);

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

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StopShake();
            }
        }
    }

    public void ShakeCamera()
    {
        if (_cbmcp)
            _cbmcp.m_AmplitudeGain = shakeIntensity;
        timer = shakeTime;
    }
    void StopShake()
    {
        _cbmcp.m_AmplitudeGain = 0;
        timer = 0;
    }
}
