using UnityEngine;

[CreateAssetMenu(fileName = "New Complete Character", menuName = "Complete Character")]
public class SO_CompleteCharacter : ScriptableObject
{
    // ~~ 1. Holds details about the full character body
    public CharacterPart[] characterParts;
}

[System.Serializable]
public class CharacterPart
{
    public string characterPartName;
    public SO_CharacterPart characterPart;
}
