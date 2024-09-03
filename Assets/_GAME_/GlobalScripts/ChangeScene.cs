using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public LevelLoader levelLoader;
    public SceneField nextScene;
    public void SelectScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void NextScene()
    {
        levelLoader.LoadNextLevel(nextScene);
    }
}
