using UnityEngine;

public class DDOL : MonoBehaviour
{
    private GameObject music;
    void Awake()
    {
        music = GameObject.Find("BGMusic");
        if(music)
        {
            
        }
        DontDestroyOnLoad(transform.gameObject);
    }
}