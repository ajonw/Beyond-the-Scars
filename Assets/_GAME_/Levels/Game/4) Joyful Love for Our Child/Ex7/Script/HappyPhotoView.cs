using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyPhotoView : MonoBehaviour
{
    public GameObject happyCanvas;
    // Start is called before the first frame update
    void Start()
    {
        happyCanvas.SetActive(false);
    }
    public void OpenCanvas()
    {
        happyCanvas.SetActive(true);
    }
    public void CloseCanvas()
    {
        happyCanvas.SetActive(false);
    }
}
