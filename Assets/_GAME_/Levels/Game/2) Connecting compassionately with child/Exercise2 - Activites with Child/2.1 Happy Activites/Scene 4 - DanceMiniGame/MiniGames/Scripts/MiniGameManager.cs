using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public GameObject musicSelectorPanel;
    public LevelLoader levelLoader;
    public SceneField originScene;
    public static MiniGameManager instance;
    public MusicController musicController;
    public BeatScroller beatScroller;

    public GameObject scorePanel;
    public TMP_Text finalScore;

    public Animator characterAnimators;

    public int currentScore;
    public int scorePerNote = 100;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public TMP_Text scoreText;
    public TMP_Text multiText;

    public bool restart = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.enabled = false;
        multiText.enabled = false;
        currentMultiplier = 1;
        scorePanel.SetActive(false);
        musicSelectorPanel.SetActive(true);
    }


    public void StartGame()
    {
        beatScroller.StartGame();
        musicController.PlayAudio();
        scoreText.enabled = true;
        multiText.enabled = true;
        scoreText.text = "Score: 0";
        multiText.text = "Multiplier: x1";
        characterAnimators.SetBool("isDancing", true);
        musicSelectorPanel.SetActive(false);
        restart = false;
    }

    public void Restart()
    {
        scorePanel.SetActive(false);
        musicSelectorPanel.SetActive(true);
        currentScore = 0;
        currentMultiplier = 1;
        multiplierTracker = 0;
        restart = true;
    }

    public void EndGame()
    {
        musicSelectorPanel.SetActive(false);
        musicController.StopAudio();
        beatScroller.StopGame();
        scorePanel.SetActive(true);
        finalScore.text = currentScore.ToString();
    }

    public void Quit()
    {
        musicSelectorPanel.SetActive(false);
        scorePanel.SetActive(false);
        levelLoader.LoadNextLevel(originScene);
        HappyActivityCompletionData.completedDance = true;
        HappyActivityCompletionData.firstTime = false;
        HappyActivityCompletionData.xVal = 15f;
        HappyActivityCompletionData.yVal = 10.8f;
    }

    public void NoteHit()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore.ToString();
        multiText.text = "Multiplier: x" + currentMultiplier;
    }

    public void NoteMissed()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;
    }
}
