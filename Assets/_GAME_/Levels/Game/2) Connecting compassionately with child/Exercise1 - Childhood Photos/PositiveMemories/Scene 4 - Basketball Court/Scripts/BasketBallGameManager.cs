using TMPro;
using UnityEngine;

public class BasketBallGameManager : MonoBehaviour
{
    public TMP_Text screenScoreText;
    public GameObject endCanvas;
    public TMP_Text finalScoreText;
    public float currentScore = 0f;

    public LevelLoader levelLoader;
    public SceneField nextScene;

    public DragController dragController;
    // Start is called before the first frame update
    void Start()
    {
        endCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndGame()
    {
        endCanvas.SetActive(true);
        finalScoreText.text = currentScore.ToString();
        dragController.DisableGame();
    }

    public void UpdateScore()
    {
        currentScore += 2;
        screenScoreText.text = "Score: " + currentScore.ToString();
    }
    public void Quit()
    {
        PositiveMemoriesCompletionData.xVal = -22f;
        PositiveMemoriesCompletionData.yVal = -13f;
        PositiveMemoriesCompletionData.completedBasketball = true;
        levelLoader.LoadNextLevel(nextScene);
    }
}
