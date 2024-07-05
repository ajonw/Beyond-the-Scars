using UnityEngine;

[CreateAssetMenu]
public class SO_Dialogue : ScriptableObject
{
    [SerializeField] public DialogueActors[] actors;

    [Tooltip("Only required if random selected as the actor name")]
    [Header("Random Actor Info")]
    public string randomActorName;
    public Sprite randomActorPortrait;

    [Header("Dialogue")]
    [TextArea]
    public string[] dialogue;

    [Tooltip("Text on the options section")]
    public string[] optionText;

    public SO_Dialogue option0;
    public SO_Dialogue option1;
    public SO_Dialogue option2;
    public SO_Dialogue option3;

}
