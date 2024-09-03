using UnityEngine;

public class Ex2CompletionData : MonoBehaviour
{
    public static bool firstTime { get; set; }
    public static bool completedHappyActivity { get; set; }
    public static bool completedUnhappyActivity { get; set; }

    static Ex2CompletionData()
    {
        firstTime = true;
        completedHappyActivity = false;
        completedUnhappyActivity = false;
    }
}
