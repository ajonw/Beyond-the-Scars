using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AdultHugsChildCutscene : MonoBehaviour
{
    [SerializeField] public GameObject player;
    private Transform playerTransform;
    private Animator characterAnimation;
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
        playerTransform = player.transform;
        characterAnimation = player.GetComponent<Animator>();
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(2f);
        //Start dialogue
        yield return StartCoroutine(StartDialogue());


        yield return new WaitForSeconds(10f);
    }

    private IEnumerator MovePlayerToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(playerTransform.position, targetPosition) > 0.1f)
        {
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPosition, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        playerTransform.position = targetPosition; // Ensure the player reaches the exact position
        yield return null;
    }

    private IEnumerator StartDialogue()
    {
        dialogueManager.InitiateDialogueForCutscene(dialogues[0]);
        while (dialogueManager.dialogueCompleted())
            yield return null;
    }
}
