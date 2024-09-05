using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllStaticData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RewardSystem.secureAttachmentLevel = 0;

        CompletionData.firstTime = true;
        CompletionData.completedHappyMemory = false;
        CompletionData.completedUnhappyMemory = false;

        PositiveMemoriesCompletionData.firstTime = true;
        PositiveMemoriesCompletionData.completedArt = false;
        PositiveMemoriesCompletionData.completedBasketball = false;

        Ex2CompletionData.firstTime = true;
        Ex2CompletionData.completedHappyActivity = false;
        Ex2CompletionData.completedUnhappyActivity = false;

        HappyActivityCompletionData.firstTime = true;
        HappyActivityCompletionData.completedEmbrace = false;
        HappyActivityCompletionData.completedPlay = false;
        HappyActivityCompletionData.completedDance = false;

        Part3CompletionData.firstTime = true;
        Part3CompletionData.justCompletedSong = false;
        Part3CompletionData.justCompletedExpressLove = false;
        Part3CompletionData.justCompletedPledge = false;
        Part3CompletionData.justCompletedRestoreEmotionalWorld = false;

    }
}
