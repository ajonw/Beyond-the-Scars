using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public GameObject selectorCanvas;


    private void Start()
    {
        selectorCanvas.SetActive(false);
    }

    public void OpenMenu()
    {
        selectorCanvas.SetActive(true);
    }
    public void CloseMenu()
    {
        selectorCanvas.SetActive(false);
    }


    public void openURL(int index)
    {
        if (index == 0)
        {
            Application.OpenURL("https://www.youtube.com/watch?v=TGVcxENdAeo");
        }
        else if (index == 1)
        {
            Application.OpenURL("https://www.youtube.com/watch?v=hNKyUp3PYDE");
        }
        else if (index == 2)
        {
            Application.OpenURL("https://www.youtube.com/watch?v=ydAyvvDQrgY");
        }
    }

}
