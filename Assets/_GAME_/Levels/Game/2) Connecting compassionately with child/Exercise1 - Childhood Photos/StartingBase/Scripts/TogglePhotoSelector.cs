using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePhotoSelector : MonoBehaviour
{
    [SerializeField] public GameObject photoSelectorMenu;
    [SerializeField] public Player_Controller playerController;
    // Start is called before the first frame update
    void Start()
    {
        photoSelectorMenu.SetActive(false);
    }

    public void openMenu()
    {
        photoSelectorMenu.SetActive(true);
        if (playerController != null)
            playerController.enabled = false;
    }

    public void closeMenu()
    {
        photoSelectorMenu.SetActive(false);
        if (playerController != null)
            playerController.enabled = true;
    }

}
