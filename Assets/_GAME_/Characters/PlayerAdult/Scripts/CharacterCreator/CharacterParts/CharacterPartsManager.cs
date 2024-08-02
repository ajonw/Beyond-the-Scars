using System.Collections.Generic;
using UnityEngine;

public class CharacterPartsManager : MonoBehaviour
{
    // ~~ 1. Updates All Animations to Match Player Selections

    [SerializeField] private SO_CompleteCharacter completeCharacter;

    // String Arrays
    [SerializeField] private string[] characterPartTypes;
    [SerializeField] private string[] characterStates;
    [SerializeField] private string[] characterDirections;

    // Animation
    private Animator animator;
    private AnimationClip animationClip;
    private AnimatorOverrideController animatorOverrideController;
    private AnimationClipOverrides defaultAnimationClips;

    private void Start()
    {
        // Set animator
        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;

        defaultAnimationClips = new AnimationClipOverrides(animatorOverrideController.overridesCount);
        animatorOverrideController.GetOverrides(defaultAnimationClips);

        // Set body part animations
        UpdateCharacterParts();
    }

    public void UpdateCharacterParts()
    {
        // Override default animation clips with character body parts
        for (int partIndex = 0; partIndex < characterPartTypes.Length; partIndex++)
        {
            // Get current body part
            string partType = characterPartTypes[partIndex];
            // Get current body part ID
            string partID = completeCharacter.characterParts[partIndex].characterPart.characterPartAnimationID.ToString();

            for (int stateIndex = 0; stateIndex < characterStates.Length; stateIndex++)
            {
                string state = characterStates[stateIndex];
                for (int directionIndex = 0; directionIndex < characterDirections.Length; directionIndex++)
                {
                    string direction = characterDirections[directionIndex];

                    // Get players animation from player body
                    // ***NOTE: Unless Changed Here, Animation Naming Must Be: "[Type]_[Index]_[state]_[direction]" (Ex. Body_0_idle_down)
                    string animationPath = "Animations/"
                    + partType + "/" + partType + "_" + partID + "/" + partType + "_" + partID + "_" + state + "_" + direction;
                    animationClip = Resources.Load<AnimationClip>(animationPath);

                    // Override default animation
                    defaultAnimationClips[partType + "_" + 0 + "_" + state + "_" + direction] = animationClip;
                }
            }
        }

        // Apply updated animations
        animatorOverrideController.ApplyOverrides(defaultAnimationClips);
    }

    public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
    {
        public AnimationClipOverrides(int capacity) : base(capacity) { }

        public AnimationClip this[string name]
        {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; }
            set
            {
                int index = this.FindIndex(x => x.Key.name.Equals(name));
                if (index != -1)
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
            }
        }
    }
}