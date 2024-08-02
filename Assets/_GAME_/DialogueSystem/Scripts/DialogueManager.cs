using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //Reward system
    public RewardSystem rewardSystem;

    // Current conversation dialogue object
    private SO_Dialogue currentConversation;

    // Current step/element in the dialogue object
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
    private TMP_Text[] optionButtonPoints;
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

    public void temp()
    {
        Debug.Log("boogies");
    }

    private void Start()
    {
        // Find Components
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
        optionButtonPoints = new TMP_Text[optionButtons.Length];

        for (int i = 0; i < optionButtons.Length; i++)
        {
            // optionButtonTexts[i] = optionButtons[i].GetComponentInChildren<TMP_Text>();
            optionButtonTexts[i] = optionButtons[i].transform.Find("Text").GetComponent<TMP_Text>();
            optionButtonPoints[i] = optionButtons[i].transform.Find("Points").GetComponent<TMP_Text>();
        }

        // Initially turnoff dialogue canvas
        dialogueCanvas.SetActive(false);
    }


    private void Update()
    {
        // Check if dialogue already active, Z / enter (action button) is pressed and that the current dialogue step is not an option 
        //  (to prevent skipping without picking an option)
        if (dialogueActivated && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)) && !isOption)
        {
            // Cancel dialogue if no more lines or actors
            if ((stepNumber >= currentConversation.actors.Length) && !isTyping)
                EndDialogue();
            // Play dialogue or Continue Dialogue
            else
                PlayDialogue();
        }
    }

    private void PlayDialogue()
    {
        // Disable player movement when dialogue is playing
        playerInput.enabled = false;

        // If current dialogue actor is "OtherDialogue" then change the current conversation to that otherdialogue
        if (currentConversation.actors[stepNumber] == DialogueActors.OtherDialogue)
        {
            stepNumber = 0;
            currentConversation = currentConversation.AnotherDialogue;
        }

        //If its random NPC
        if (currentConversation.actors[stepNumber] == DialogueActors.Random)
            SetActorInfo(false);
        //If its a recurring NPC
        else
            SetActorInfo(true);

        // Turn on main dialogue canvas.
        dialogueCanvas.SetActive(true);

        //Turn on appropriate canvas based on the current type of conversation

        // If current conversation actor is of Player type then, turn on player dialogue box
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

            ActivateOptionButtons();
        }
        // If NPC or Random, use npc dialogue box
        else
        {
            npcDialogueBox.SetActive(true);
            playerDialogueBox.SetActive(false);
            dialogueOptionsBox.SetActive(false);
        }

        // Set current actor name and portrait
        actorName.text = currentSpeaker;
        actorPortrait.sprite = currentPortrait;
        // if conversation not yet finished move on
        if (stepNumber < currentConversation.dialogue.Length)
        {
            // If coroutine is not typing then start coroutine
            if (!isTyping)
            {
                typeDialogueCoroutine = StartCoroutine(TypeDialogueText(currentConversation.dialogue[stepNumber]));
            }
            // If coroutine is still typing, then finish typing early.
            else
            {
                FinishParagraphEarly();
            }
        }
        // If conversation is finished check if there are any options
        // If no options move on stepnumber, otherwise leave stepnumber, to not progess, but wait for option selection
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
            // Find actor information for player
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
            // Find actor information for the current actor in coversation
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
        // Random actor
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
        if (rewardSystem)
            rewardSystem.updatePoints(currentConversation.conversationPoints);
        stepNumber = 0;
        isOption = false;
        PlayDialogue();
    }

    // Called by the NPCDialogue script when player is in proximity.
    // Allows dialogue to be started by player
    public void InitiateDialogue(NPCDialogue npcDialogue)
    {
        // Read the array we're currently stepping through
        currentConversation = npcDialogue.conversation[0];
        dialogueActivated = true;
    }

    public void InitiateDialogueForCutscene(SO_Dialogue conversation)
    {
        currentConversation = conversation;
        dialogueActivated = true;
        PlayDialogue();
    }

    // End dilogue, resets step number to 0, retracts dialogue canvas and enables player movement.
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

    private void ActivateOptionButtons()
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].SetActive(true);
            optionButtons[i].GetComponent<Button>().enabled = false;
            optionButtons[i].GetComponent<EventTrigger>().enabled = false;
            optionButtons[i].transform.Find("SelectedBorder").gameObject.SetActive(false);
            optionButtons[i].transform.Find("UnselectedBorder").gameObject.SetActive(true);
        }

        for (int i = 0; i < currentConversation.optionText.Length; i++)
        {
            if (currentConversation.optionText[i] == null)
            {
                // If no option text turn off button
                optionButtons[i].SetActive(false);
            }
            else
            {
                // If there is an option
                optionButtonTexts[i].text = currentConversation.optionText[i];
                optionButtons[i].SetActive(true);
                optionButtons[i].GetComponent<Button>().enabled = true;
                optionButtons[i].GetComponent<EventTrigger>().enabled = true;

                // Check if option has points
                if (i < currentConversation.optionPoints.Length && currentConversation.optionPoints[i] != 0)
                {
                    if (currentConversation.optionPoints[i] > 0)
                    {
                        optionButtonPoints[i].text = "+" + currentConversation.optionPoints[i].ToString();
                    }
                    else
                    {
                        optionButtonPoints[i].text = currentConversation.optionPoints[i].ToString();
                    }
                }
            }
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

    public bool dialogueCompleted()
    {
        return dialogueActivated;
    }
}

//Types of actors
public enum DialogueActors
{
    Player,
    ChildPlayer,
    Companion,
    Random,
    Branch,
    OtherDialogue,
};