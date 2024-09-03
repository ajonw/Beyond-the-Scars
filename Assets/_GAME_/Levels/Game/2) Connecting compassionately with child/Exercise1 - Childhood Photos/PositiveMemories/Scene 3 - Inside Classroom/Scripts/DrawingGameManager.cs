using UnityEngine;

public class DrawingGameManager : MonoBehaviour
{
    public GameObject drawingCanvas;
    public GameObject quitButton;
    public LevelLoader levelLoader;
    public SceneField nextScene;

    public LineGenerator lineGen;
    // Start is called before the first frame update
    void Start()
    {
        drawingCanvas.SetActive(false);
        quitButton.SetActive(false);
    }

    public void StartGame()
    {
        drawingCanvas.SetActive(true);
        quitButton.SetActive(true);
        lineGen.StartGame();
    }

    public void EndGame()
    {
        levelLoader.LoadNextLevel(nextScene);
        lineGen.EndGame();
    }
}
