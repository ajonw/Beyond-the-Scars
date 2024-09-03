using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginComfortSession : MonoBehaviour
{
    public GameObject emotionSelectPanel;
    public Animator childAnimator;
    public SpeechRecognition speechRecognition;

    public int selectedIndex;
    // Start is called before the first frame update
    void Start()
    {
        emotionSelectPanel.SetActive(false);
    }

    public void BeginEmotionSelect()
    {
        emotionSelectPanel.SetActive(true);
        speechRecognition.BeginSession();
        speechRecognition.EndSession();
    }

    public void RestartEmotionSelect()
    {
        emotionSelectPanel.SetActive(true);
        speechRecognition.EndSession();
    }

    public void SelectEmotion(int index)
    {
        if (index == 0)
        {
            childAnimator.SetBool("isFearful", true);
        }
        else if (index == 1)
        {
            childAnimator.SetBool("isAngry", true);
        }
        else if (index == 2)
        {
            childAnimator.SetBool("isCrying", true);
        }
        selectedIndex = index;
        emotionSelectPanel.SetActive(false);
        speechRecognition.ShowSession();
    }

}
