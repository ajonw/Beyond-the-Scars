using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class DialogueOptionSelect : MonoBehaviour
{
    [SerializeField] public GameObject Box;
    [SerializeField] public GameObject BlurredBox;

    private void Start()
    {
        Box.SetActive(false);
        BlurredBox.SetActive(true);
    }
    public void Select()
    {
        Box.SetActive(true);
        BlurredBox.SetActive(false);
    }
    public void Unselect()
    {
        Box.SetActive(false);
        BlurredBox.SetActive(true);
    }
}
