using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using HuggingFace.API;
using UnityEngine.InputSystem.Interactions;
using Cinemachine;

public class SpeechRecognition : MonoBehaviour
{
    [SerializeField] public GameObject instructionText;
    [SerializeField] public LevelLoader levelLoader;
    [SerializeField] public SceneField nextScene;
    [SerializeField] public GameObject speechRecognitionCanvas;
    [SerializeField] public GameObject playerDialogueCanvas;
    [SerializeField] public GameObject pressZinstruction;
    [SerializeField] public Button startButton;
    [SerializeField] public Button stopButton;
    [SerializeField] public Button classifyButton;
    [SerializeField] public TMP_Text text;
    [SerializeField] public TMP_Text classificationText;

    [SerializeField] Animator childAnimator;

    [SerializeField] public SO_Dialogue[] dialogues;
    [SerializeField] public CinemachineVirtualCamera virtualCamera;
    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] public bool customResponse = false;
    [SerializeField] public BeginComfortSession comfortSession;

    [SerializeField] public GameObject button1;

    private AudioClip clip;
    private byte[] bytes;
    private bool recording;

    private bool dialogueStarted = false;


    private void Start()
    {
        speechRecognitionCanvas.SetActive(false);
        pressZinstruction.SetActive(false);
        instructionText.SetActive(false);
        playerDialogueCanvas.SetActive(false);
    }

    public void Quit()
    {
        speechRecognitionCanvas.SetActive(false);
        instructionText.SetActive(false);
        playerDialogueCanvas.SetActive(false);
        levelLoader.LoadNextLevel(nextScene);
    }

    public void EndSession()
    {
        speechRecognitionCanvas.SetActive(false);
        instructionText.SetActive(false);
        playerDialogueCanvas.SetActive(false);
    }

    public void BeginSession()
    {
        speechRecognitionCanvas.SetActive(true);
        startButton.onClick.AddListener(StartRecording);
        stopButton.onClick.AddListener(StopRecording);
        if (!customResponse)
        {
            classifyButton.onClick.AddListener(ClassifyText);
        }
        else
        {
            classifyButton.onClick.AddListener(SendText);
        }

        text.color = Color.red;
        text.text = "Press record and speak loudly";
        playerDialogueCanvas.SetActive(true);
        instructionText.SetActive(true);
    }
    public void ShowSession()
    {
        if (customResponse)
            button1.SetActive(false);
        text.color = Color.red;
        text.text = "Press record and speak loudly";
        playerDialogueCanvas.SetActive(true);
        instructionText.SetActive(true);
        speechRecognitionCanvas.SetActive(true);
    }

    private void StartRecording()
    {
        clip = Microphone.Start(null, false, 10, 44100);
        text.color = Color.red;
        text.text = "Recording...";
        recording = true;
        if (dialogueStarted)
        {
            playerDialogueCanvas.SetActive(true);
            dialogueManager.EndDialogue();
        }
        dialogueStarted = false;
    }

    private void Update()
    {
        if (recording && Microphone.GetPosition(null) >= clip.samples)
        {
            StopRecording();
        }
    }

    private void StopRecording()
    {
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        recording = false;
        SendRecording();
    }

    private void SendRecording()
    {
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response =>
        {
            text.color = new Color(0.58f, 0.30f, 0.08f);
            text.text = response;
        }, error =>
        {
            text.color = Color.red;
            text.text = "Error, try again in a few seconds";
        });
    }

    private void ClassifyText()
    {
        if (text && text.text != "Press record and speak loudly" && text.text != "Recording..." && text.text != "")
        {
            HuggingFaceAPI.TextClassification(text.text, response =>
        {
            classificationText.color = Color.white;
            classificationText.text = response.classifications[0].label.ToString();
            playerDialogueCanvas.SetActive(false);
            dialogueStarted = true;
            if (classificationText.text.ToLower() == "positive")
            {
                childAnimator.SetTrigger("isHappy");
                StartCoroutine(StartDialogue(0));
            }
            else if (classificationText.text.ToLower() == "negative")
            {
                childAnimator.SetTrigger("isSad");
                StartCoroutine(StartDialogue(1));
            }
        }, error =>
        {
            classificationText.color = Color.red;
            classificationText.text = "Error, try again in a few seconds";
        });
        }
        else
        {

        }
    }

    private void SendText()
    {
        if (text && text.text != "Press record and speak loudly" && text.text != "Recording..." && text.text != "")
        {
            HuggingFaceAPI.TextClassification(text.text, response =>
        {
            button1.SetActive(true);
            pressZinstruction.SetActive(true);
            classificationText.color = Color.white;
            classificationText.text = response.classifications[0].label.ToString();
            playerDialogueCanvas.SetActive(false);
            dialogueStarted = true;
            if (classificationText.text.ToLower() == "positive")
            {
                if (comfortSession.selectedIndex == 0)
                {
                    childAnimator.SetBool("isFearful", false);
                    StartCoroutine(StartDialogue(0));
                }
                else if (comfortSession.selectedIndex == 1)
                {
                    childAnimator.SetBool("isAngry", false);
                    StartCoroutine(StartDialogue(1));
                }
                else if (comfortSession.selectedIndex == 2)
                {
                    childAnimator.SetBool("isCrying", false);
                    StartCoroutine(StartDialogue(2));
                }

            }
            else if (classificationText.text.ToLower() == "negative")
            {
                StartCoroutine(StartDialogue(3));
            }
        }, error =>
        {
            classificationText.color = Color.red;
            classificationText.text = "Error, try again in a few seconds";
        });
        }
        else
        {

        }
    }

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels)
    {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2))
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);

                foreach (var sample in samples)
                {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }

    private IEnumerator StartDialogue(int index)
    {
        yield return new WaitForSeconds(0);
        dialogueManager.InitiateDialogueForCutscene(dialogues[index]);
        while (dialogueManager.dialogueCompleted())
        {
            yield return null;
        }
    }


    public void PrimeAPI()
    {
        HuggingFaceAPI.TextClassification(text.text, response =>
      { }, error =>
      { });
    }
}
