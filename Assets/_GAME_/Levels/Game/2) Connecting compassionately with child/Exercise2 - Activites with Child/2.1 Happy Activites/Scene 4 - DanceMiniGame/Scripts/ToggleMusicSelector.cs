using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMusicSelector : MonoBehaviour
{
    [SerializeField] public GameObject musicSelectorMenu;
    [SerializeField] public Player_Controller playerController;
    // Start is called before the first frame update
    void Start()
    {
        musicSelectorMenu.SetActive(false);
    }

    public void openMenu()
    {
        musicSelectorMenu.SetActive(true);
        if (playerController != null)
            playerController.enabled = false;
    }

    public void closeMenu()
    {
        musicSelectorMenu.SetActive(false);
        if (playerController != null)
            playerController.enabled = true;
    }

}
