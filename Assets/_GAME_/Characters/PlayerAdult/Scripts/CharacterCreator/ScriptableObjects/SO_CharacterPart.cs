using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Part", menuName = "Character Part")]
public class SO_CharacterPart : ScriptableObject
{
    // ~~ 1. Holds details about a character part's animations

    // Body Part Details
    public string characterPartName;
    public int characterPartAnimationID;

    // List Containing All Body Part Animations
    public List<AnimationClip> allCharacterPartAnimations = new List<AnimationClip>();
}