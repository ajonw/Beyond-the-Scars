using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CompletionData
{
    public static bool firstTime { get; set; }
    public static bool completedHappyMemory { get; set; }
    public static bool completedUnhappyMemory { get; set; }

    static CompletionData()
    {
        firstTime = true;
        completedHappyMemory = false;
        completedUnhappyMemory = false;
    }
}
