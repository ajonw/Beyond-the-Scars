using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void SelectScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
