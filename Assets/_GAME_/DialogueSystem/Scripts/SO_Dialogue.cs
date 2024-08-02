using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class SO_Dialogue : ScriptableObject
{
    [Header("Conversation Points")]
    public int conversationPoints = 0;
    [Header("Actors")]
    [SerializeField] public DialogueActors[] actors;

    [Tooltip("Only required if random selected as the actor name")]
    [Header("Random Actor Info")]
    public string randomActorName;
    public Sprite randomActorPortrait;

    [Header("Dialogue")]
    [TextArea]
    public string[] dialogue;

    [Tooltip("Text on the options section")]
    [Header("Dialogue Options for Branch")]
    public string[] optionText;

    public int[] optionPoints;

    public SO_Dialogue option0;
    public SO_Dialogue option1;
    public SO_Dialogue option2;
    public SO_Dialogue option3;

    [Header("Another Dialogue to Play")]
    public SO_Dialogue AnotherDialogue;

}
