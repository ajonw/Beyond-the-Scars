using UnityEngine;

public class ArcadeQuestLogManager : MonoBehaviour
{
    public GameObject embraceChecked;
    public GameObject embraceUnchecked;
    public GameObject embraceQuestLog;
    public TriggerTransition embraceTransition;

    public GameObject playChecked;
    public GameObject playUnchecked;
    public GameObject playQuestLog;
    public TriggerTransition playTransition;

    public GameObject danceChecked;
    public GameObject danceUnchecked;
    public GameObject danceQuestLog;
    public TriggerTransition danceTransition;

    // Start is called before the first frame update
    void Start()
    {
        embraceQuestLog.SetActive(true);

        if (HappyActivityCompletionData.completedEmbrace)
        {
            embraceChecked.SetActive(true);
            embraceUnchecked.SetActive(false);
            embraceTransition.DisableTransition();

            playQuestLog.SetActive(true);
            playTransition.EnableTransition();

            danceQuestLog.SetActive(true);
            danceTransition.EnableTransition();
        }
        else
        {
            embraceChecked.SetActive(false);
            embraceUnchecked.SetActive(true);
            embraceTransition.EnableTransition();

            playQuestLog.SetActive(false);
            playTransition.DisableTransition();

            danceQuestLog.SetActive(false);
            danceTransition.DisableTransition();
        }

        if (HappyActivityCompletionData.completedPlay)
        {
            playChecked.SetActive(true);
            playUnchecked.SetActive(false);
            playTransition.DisableTransition();
        }
        else
        {
            playChecked.SetActive(false);
            playUnchecked.SetActive(true);
        }

        if (HappyActivityCompletionData.completedDance)
        {
            danceChecked.SetActive(true);
            danceUnchecked.SetActive(false);
            danceTransition.DisableTransition();
        }
        else
        {
            danceChecked.SetActive(false);
            danceUnchecked.SetActive(true);
        }
    }

}
