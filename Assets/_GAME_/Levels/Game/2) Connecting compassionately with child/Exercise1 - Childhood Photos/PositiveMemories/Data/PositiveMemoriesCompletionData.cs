using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PositiveMemoriesCompletionData
{
    public static bool firstTime { get; set; }
    public static bool completedBasketball { get; set; }
    public static bool completedArt { get; set; }

    public static float xVal { get; set; }

    public static float yVal { get; set; }
    static PositiveMemoriesCompletionData()
    {
        firstTime = true;
        completedBasketball = false;
        completedArt = false;
        xVal = -5f;
        yVal = -13f;
    }
}
