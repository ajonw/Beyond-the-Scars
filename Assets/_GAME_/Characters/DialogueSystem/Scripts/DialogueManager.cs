using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private SO_Dialogue currentConversation;
    private int stepNumber;
    private bool dialogueActivated;


    //UI References

    private GameObject dialogueCanvas;
    private GameObject playerDialogueBox;
    private GameObject npcDialogueBox;
    private TMP_Text actorName;
    private TMP_Text npcDialogueText;
    private TMP_Text playerDialogueText;
    private Image actorPortrait;

    //Button references
    private GameObject[] optionButtons;
    private TMP_Text[] optionButtonTexts;
    private GameObject dialogueOptionsBox;


    //current speaker
    private string currentSpeaker;
    private Sprite currentPortrait;
    public SO_Actor[] actorsSO;

    //Type write
    [SerializeField] private float typingSpeed = 8;
    private bool isTyping;
    private Coroutine typeDialogueCoroutine;
    private const string HTML_Alpha = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

    //Player freeze
    private PlayerInput playerInput;

    private bool isOption = false;

    void Start()
    {
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();

        dialogueCanvas = GameObject.Find("DialogueCanvas");
        playerDialogueBox = GameObject.Find("PlayerDialogueBox");
        npcDialogueBox = GameObject.Find("NPCDialogueBox");


        actorName = GameObject.Find("CharacterName").GetComponent<TMP_Text>();
        actorPortrait = GameObject.Find("CharacterPortrait").GetComponent<Image>();
        npcDialogueText = GameObject.Find("NPCDialogueText").GetComponent<TMP_Text>();
        playerDialogueText = GameObject.Find("PlayerDialogueText").GetComponent<TMP_Text>();

        //Find option buttons
        optionButtons = new GameObject[4];
        optionButtons[0] = GameObject.Find("Option0");
        optionButtons[1] = GameObject.Find("Option1");
        optionButtons[2] = GameObject.Find("Option2");
        optionButtons[3] = GameObject.Find("Option3");
        dialogueOptionsBox = GameObject.Find("DialogueOptionsBox");

        //Find the tmptext on the buttons
        optionButtonTexts = new TMP_Text[optionButtons.Length];

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtonTexts[i] = optionButtons[i].GetComponentInChildren<TMP_Text>();
        }

        dialogueCanvas.SetActive(false);
    }


    void Update()
    {
        if (dialogueActivated && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)) && !isOption)
        {
            // Cancel dialogue if no more lines or actors
            if ((stepNumber >= currentConversation.actors.Length) && !isTyping)
                EndDialogue();
            //Continue Dialogue
            else
                PlayDialogue();
        }
    }

    private void PlayDialogue()
    {
        playerInput.enabled = false;
        //If its random NPC
        if (currentConversation.actors[stepNumber] == DialogueActors.Random)
            SetActorInfo(false);
        //If its a recurring NPC
        else
            SetActorInfo(true);

        dialogueCanvas.SetActive(true);
        //Turn on appropriate canvas
        //Turn player canvas on
        if (currentConversation.actors[stepNumber] == DialogueActors.Player)
        {
            playerDialogueBox.SetActive(true);
            npcDialogueBox.SetActive(false);
            dialogueOptionsBox.SetActive(false);
        }
        //If branch options, turn on options canvas
        else if (currentConversation.actors[stepNumber] == DialogueActors.Branch)
        {
            isOption = true;
            dialogueOptionsBox.SetActive(true);
            npcDialogueBox.SetActive(false);
            playerDialogueBox.SetActive(false);

            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].SetActive(true);
            }

            if (currentConversation.option0 == null)
            {
                Button button = GameObject.Find("Option0").GetComponent<Button>();
                EventTrigger et = GameObject.Find("Option0").GetComponent<EventTrigger>();
                button.enabled = false;
                et.enabled = false;
            }
            if (currentConversation.option1 == null)
            {
                Button button = GameObject.Find("Option1").GetComponent<Button>();
                EventTrigger et = GameObject.Find("Option1").GetComponent<EventTrigger>();
                button.enabled = false;
                et.enabled = false;
            }
            if (currentConversation.option2 == null)
            {
                Button button = GameObject.Find("Option2").GetComponent<Button>();
                EventTrigger et = GameObject.Find("Option2").GetComponent<EventTrigger>();
                button.enabled = false;
                et.enabled = false;
            }
            if (currentConversation.option3 == null)
            {
                Button button = GameObject.Find("Option3").GetComponent<Button>();
                EventTrigger et = GameObject.Find("Option3").GetComponent<EventTrigger>();
                button.enabled = false;
                et.enabled = false;
            }

            for (int i = 0; i < currentConversation.optionText.Length; i++)
            {
                if (currentConversation.optionText[i] == null)
                {
                    optionButtons[i].SetActive(false);
                }
                else
                {
                    optionButtonTexts[i].text = currentConversation.optionText[i];
                    optionButtons[i].SetActive(true);
                }
            }
            //Set first button to be auto selected
            //optionButtons[0].GetComponent<Button>().Select();
        }
        //Any NPC, turn on npc dialogue canvas
        else
        {
            npcDialogueBox.SetActive(true);
            playerDialogueBox.SetActive(false);
            dialogueOptionsBox.SetActive(false);
        }

        //Display Dialogue
        actorName.text = currentSpeaker;
        actorPortrait.sprite = currentPortrait;


        if (stepNumber < currentConversation.dialogue.Length)
        {
            //Update Conversation Text
            if (!isTyping)
            {
                typeDialogueCoroutine = StartCoroutine(TypeDialogueText(currentConversation.dialogue[stepNumber]));
            }
            else
            {
                FinishParagraphEarly();
            }
        }
        else if (currentConversation.option0 != null)
        {
            stepNumber++;
        }
    }

    private void SetActorInfo(bool recurringNPC)
    {
        currentSpeaker = "";
        currentPortrait = null;
        //If branching option, actor is always player
        if (currentConversation.actors[stepNumber] == DialogueActors.Branch)
        {
            for (int i = 0; i < actorsSO.Length; i++)
            {
                if (actorsSO[i].actorType == DialogueActors.Player)
                {
                    currentSpeaker = actorsSO[i].actorName;
                    currentPortrait = actorsSO[i].actorPortrait;
                    break;
                }
            }
        }
        else if (recurringNPC)
        {
            for (int i = 0; i < actorsSO.Length; i++)
            {
                if (actorsSO[i].actorType == currentConversation.actors[stepNumber])
                {
                    currentSpeaker = actorsSO[i].actorName;
                    currentPortrait = actorsSO[i].actorPortrait;
                    break;
                }
            }
        }
        else
        {
            currentSpeaker = currentConversation.randomActorName;
            currentPortrait = currentConversation.randomActorPortrait;
        }
    }

    public void Option(int optionNumber)
    {
        foreach (GameObject button in optionButtons)
        {
            button.SetActive(false);
        }

        if (optionNumber == 0)
        {
            currentConversation = currentConversation.option0;
        }
        if (optionNumber == 1)
        {
            currentConversation = currentConversation.option1;
        }
        if (optionNumber == 2)
        {
            currentConversation = currentConversation.option2;
        }
        if (optionNumber == 3)
        {
            currentConversation = currentConversation.option3;
        }
        stepNumber = 0;
        isOption = false;
        PlayDialogue();
    }


    public void InitiateDialogue(NPCDialogue npcDialogue)
    {
        // Read the array we're currently stepping through
        currentConversation = npcDialogue.conversation[0];
        dialogueActivated = true;
    }

    public void EndDialogue()
    {
        playerInput.enabled = true;
        //Reset index 
        stepNumber = 0;
        dialogueActivated = false;

        if (!dialogueCanvas.IsDestroyed())
        {
            dialogueCanvas.SetActive(false);
        }
    }

    //To write text letter by letter
    private IEnumerator TypeDialogueText(string p)
    {
        isTyping = true;

        TMP_Text currentDialogueText = (currentConversation.actors[stepNumber] == DialogueActors.Player) ?
        playerDialogueText : npcDialogueText;

        currentDialogueText.text = "";

        string originalText = p;
        string displayedText = "";
        int alphaIndex = 0;

        yield return new WaitForSeconds(0.5f);
        foreach (char c in p.ToCharArray())
        {
            alphaIndex++;
            currentDialogueText.text = originalText;
            displayedText = currentDialogueText.text.Insert(alphaIndex, HTML_Alpha);
            currentDialogueText.text = displayedText;

            yield return new WaitForSeconds(MAX_TYPE_TIME / typingSpeed);
        }

        isTyping = false;
        stepNumber++;
    }

    private void FinishParagraphEarly()
    {
        //stop Coroutine
        StopCoroutine(typeDialogueCoroutine);
        //finish displaying text
        TMP_Text currentDialogueText = (currentConversation.actors[stepNumber] == DialogueActors.Player) ?
        playerDialogueText : npcDialogueText;
        currentDialogueText.text = currentConversation.dialogue[stepNumber];

        //update istyping
        isTyping = false;
        stepNumber++;
    }
}

//Types of actors
public enum DialogueActors
{
    Player,
    Companion,
    Random,
    Branch
};