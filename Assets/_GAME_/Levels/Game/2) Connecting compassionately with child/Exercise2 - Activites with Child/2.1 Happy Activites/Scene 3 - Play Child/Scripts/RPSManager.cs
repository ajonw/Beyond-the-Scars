using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class RPSManager : MonoBehaviour
{
    public GameObject gamePanel;
    public GameObject endButton;
    public Image AIChoice;
    public TMP_Text Result;
    public LevelLoader levelLoader;
    public SceneField nextScene;

    public Animator childAnimator;

    public string[] Choices;

    public Sprite Rock, Paper, Scissor;

    void Start()
    {
        gamePanel.SetActive(false);
        endButton.SetActive(false);
    }

    public void StartGame()
    {
        gamePanel.SetActive(true);
        endButton.SetActive(true);
    }

    public void EndGame()
    {
        gamePanel.SetActive(false);
        HappyActivityCompletionData.firstTime = false;
        HappyActivityCompletionData.completedPlay = true;
        HappyActivityCompletionData.xVal = 1.89f;
        HappyActivityCompletionData.yVal = 6.69f;
        levelLoader.LoadNextLevel(nextScene);
    }

    public void Play(string myChoice)
    {
        string randomChoice = Choices[Random.Range(0, Choices.Length)];
        switch (randomChoice)
        {
            case "Rock":
                AIChoice.sprite = Rock;
                switch (myChoice)
                {
                    case "Rock":
                        Result.text = "Tie";
                        break;
                    case "Paper":
                        childAnimator.SetTrigger("isSad");
                        Result.text = "You Won!";
                        break;
                    case "Scissor":
                        childAnimator.SetTrigger("isHappy");
                        Result.text = "You Lost";
                        break;
                }
                break;
            case "Paper":
                AIChoice.sprite = Paper;
                switch (myChoice)
                {
                    case "Rock":
                        childAnimator.SetTrigger("isHappy");
                        Result.text = "You Lost";
                        break;
                    case "Paper":
                        Result.text = "Tie";
                        break;
                    case "Scissor":
                        childAnimator.SetTrigger("isSad");
                        Result.text = "You Won!";
                        break;
                }
                break;
            case "Scissor":
                AIChoice.sprite = Scissor;
                switch (myChoice)
                {
                    case "Rock":
                        childAnimator.SetTrigger("isSad");
                        Result.text = "You Won!";
                        break;
                    case "Paper":
                        childAnimator.SetTrigger("isHappy");
                        Result.text = "You Lost";
                        break;
                    case "Scissor":
                        Result.text = "Tie";
                        break;
                }
                break;
        }
    }
}