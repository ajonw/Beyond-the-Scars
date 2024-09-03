using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyActivityCompletionData : MonoBehaviour
{
    public static bool firstTime { get; set; }
    public static bool completedEmbrace { get; set; }
    public static bool completedPlay { get; set; }
    public static bool completedDance { get; set; }

    public static float xVal { get; set; }

    public static float yVal { get; set; }

    static HappyActivityCompletionData()
    {
        firstTime = true;
        completedEmbrace = false;
        completedPlay = false;
        completedDance = false;
        xVal = -6.02f;
        yVal = -6.46f;
    }
}
