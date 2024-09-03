using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part3CompletionData : MonoBehaviour
{
    public static bool firstTime { get; set; }
    public static bool justCompletedSong { get; set; }
    public static bool justCompletedExpressLove { get; set; }
    public static bool justCompletedPledge { get; set; }

    public static bool justCompletedRestoreEmotionalWorld { get; set; }

    static Part3CompletionData()
    {
        firstTime = true;
        justCompletedSong = false;
        justCompletedExpressLove = false;
        justCompletedPledge = false;
        justCompletedRestoreEmotionalWorld = false;
    }
}
