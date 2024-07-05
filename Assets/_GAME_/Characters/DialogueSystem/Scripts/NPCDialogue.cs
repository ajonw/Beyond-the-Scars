using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public SO_Dialogue[] conversation;
    private Transform player;
    private SpriteRenderer speechIcon;

    private DialogueManager dialogueManager;

    private bool dialogueInitiated;
    private void Start()
    {
        speechIcon = GetComponent<SpriteRenderer>();
        speechIcon.enabled = false;
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !dialogueInitiated)
        {
            speechIcon.enabled = true;
            player = collision.gameObject.GetComponent<Transform>();
            dialogueManager.InitiateDialogue(this);
            dialogueInitiated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speechIcon.enabled = false;
            // playerInProximity = false;

            dialogueManager.EndDialogue();
            dialogueInitiated = false;
        }
    }
}
