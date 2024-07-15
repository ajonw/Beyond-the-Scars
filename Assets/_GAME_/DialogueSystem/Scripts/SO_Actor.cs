using UnityEngine;

[CreateAssetMenu]
public class SO_Actor : ScriptableObject
{

    [SerializeField] public DialogueActors actorType;
    [SerializeField] public string actorName;
    [SerializeField] public Sprite actorPortrait;
}
