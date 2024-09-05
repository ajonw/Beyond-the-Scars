using UnityEngine;
public class Destroyer : MonoBehaviour
{
    public bool destroyPrimaryMusic = true;
    public bool destroyPrimary2Music = true;
    public bool destroySecondaryMusic = true;
    public bool destroyTertiaryMusic = true;
    void Awake()
    {

        GameObject backgroundMusic;

        if (destroyPrimaryMusic)
        {
            backgroundMusic = GameObject.Find("BGMusic");
            if (backgroundMusic && backgroundMusic.scene.name == "DontDestroyOnLoad")
            {
                Destroy(backgroundMusic);
                backgroundMusic = null;
            }
        }
        if (destroyPrimary2Music)
        {
            backgroundMusic = GameObject.Find("BG1Music");
            if (backgroundMusic && backgroundMusic.scene.name == "DontDestroyOnLoad")
            {
                Destroy(backgroundMusic);
                backgroundMusic = null;
            }
        }
        if (destroySecondaryMusic)
        {
            backgroundMusic = GameObject.Find("BG2Music");
            if (backgroundMusic && backgroundMusic.scene.name == "DontDestroyOnLoad")
            {
                Destroy(backgroundMusic);
                backgroundMusic = null;
            }
        }
        if (destroyTertiaryMusic)
        {
            backgroundMusic = GameObject.Find("BG3Music");
            if (backgroundMusic && backgroundMusic.scene.name == "DontDestroyOnLoad")
            {
                Destroy(backgroundMusic);
                backgroundMusic = null;
            }
        }
    }
}