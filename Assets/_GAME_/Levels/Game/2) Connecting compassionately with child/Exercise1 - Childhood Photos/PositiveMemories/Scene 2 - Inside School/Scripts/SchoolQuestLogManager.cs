using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolQuestLogManager : MonoBehaviour
{
    public GameObject basketballChecked;
    public GameObject basketballUnchecked;
    public GameObject artChecked;
    public GameObject artUnchecked;

    public EnterDoor artClassTransition;
    public TriggerTransition basketballTransition;

    // Start is called before the first frame update
    void Start()
    {
        if (PositiveMemoriesCompletionData.completedBasketball)
        {
            basketballChecked.SetActive(true);
            basketballUnchecked.SetActive(false);
            basketballTransition.DisableTransition();
        }
        else
        {
            basketballChecked.SetActive(false);
            basketballUnchecked.SetActive(true);
            basketballTransition.EnableTransition();
        }

        if (PositiveMemoriesCompletionData.completedArt)
        {
            artChecked.SetActive(true);
            artUnchecked.SetActive(false);
            artClassTransition.DisableDoor();
        }
        else
        {
            artChecked.SetActive(false);
            artUnchecked.SetActive(true);
            artClassTransition.EnableDoor();
        }
    }

}
