using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log(SceneManager.GetActiveScene().name);
        print(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "CharacterCreator")
        {
            BackgroundMusic.backgroundMusic.GetComponent<AudioSource>().Pause();
        }


    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit");
    }
}
