
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPartsSelector : MonoBehaviour
{
    // ~~ 1. Handles Character Part Selection Updates

    // Full Character Body
    [SerializeField] private SO_CompleteCharacter characterBody;
    // Body Part Selections
    [SerializeField] private CharacterPartSelection[] characterPartSelections;

    private void Start()
    {
        // Get All Current character Parts
        for (int i = 0; i < characterPartSelections.Length; i++)
        {
            GetCurrentCharacterParts(i);
        }
    }

    public void NextCharacterPart(int partIndex)
    {
        if (ValidateIndexValue(partIndex))
        {
            if (characterPartSelections[partIndex].characterPartCurrentIndex < characterPartSelections[partIndex].characterPartOptions.Length - 1)
            {
                characterPartSelections[partIndex].characterPartCurrentIndex++;
            }
            else
            {
                characterPartSelections[partIndex].characterPartCurrentIndex = 0;
            }

            UpdateCurrentPart(partIndex);
        }
    }

    public void PreviousCharacterPart(int partIndex)
    {
        if (ValidateIndexValue(partIndex))
        {
            if (characterPartSelections[partIndex].characterPartCurrentIndex > 0)
            {
                characterPartSelections[partIndex].characterPartCurrentIndex--;
            }
            else
            {
                characterPartSelections[partIndex].characterPartCurrentIndex = characterPartSelections[partIndex].characterPartOptions.Length - 1;
            }

            UpdateCurrentPart(partIndex);
        }
    }

    private bool ValidateIndexValue(int partIndex)
    {
        if (partIndex > characterPartSelections.Length || partIndex < 0)
        {
            Debug.Log("Index value does not match any body parts!");
            return false;
        }
        else
        {
            return true;
        }
    }

    private void GetCurrentCharacterParts(int partIndex)
    {
        // Get Current Body Part Name
        characterPartSelections[partIndex].characterPartNameTextComponent.text = characterBody.characterParts[partIndex].characterPart.characterPartName;
        // Get Current Body Part Animation ID
        characterPartSelections[partIndex].characterPartCurrentIndex = characterBody.characterParts[partIndex].characterPart.characterPartAnimationID;
    }

    private void UpdateCurrentPart(int partIndex)
    {
        // Update Selection Name Text
        characterPartSelections[partIndex].characterPartNameTextComponent.text = characterPartSelections[partIndex].characterPartOptions[characterPartSelections[partIndex].characterPartCurrentIndex].characterPartName;
        // Update Character Body Part
        characterBody.characterParts[partIndex].characterPart = characterPartSelections[partIndex].characterPartOptions[characterPartSelections[partIndex].characterPartCurrentIndex];
    }
}

[System.Serializable]
public class CharacterPartSelection
{
    public string characterPartName;
    public SO_CharacterPart[] characterPartOptions;
    public TMP_Text characterPartNameTextComponent;
    [HideInInspector] public int characterPartCurrentIndex;
}