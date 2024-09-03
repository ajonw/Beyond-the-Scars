using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public SO_Dialogue[] conversation;
    public GameObject instructionText;
    private Transform player;
    private SpriteRenderer speechIcon;

    private DialogueManager dialogueManager;

    public bool onTriggerBubble = true;

    private bool dialogueInitiated;
    private void Start()
    {
        speechIcon = GetComponent<SpriteRenderer>();
        speechIcon.enabled = false;
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        if (instructionText)
            instructionText.SetActive(false);
    }

    public void ResetDialogueInitiated()
    {
        dialogueInitiated = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !dialogueInitiated && onTriggerBubble)
        {
            speechIcon.enabled = true;
            player = collision.gameObject.GetComponent<Transform>();
            dialogueManager.InitiateDialogue(this);
            dialogueInitiated = true;
            if (instructionText)
                instructionText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && onTriggerBubble)
        {
            speechIcon.enabled = false;
            // playerInProximity = false;

            dialogueManager.EndDialogue();
            dialogueInitiated = false;
            if (instructionText)
                instructionText.SetActive(false);
        }
    }
}
