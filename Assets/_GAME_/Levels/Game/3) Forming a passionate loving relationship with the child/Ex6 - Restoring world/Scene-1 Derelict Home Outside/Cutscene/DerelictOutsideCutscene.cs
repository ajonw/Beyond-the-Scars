using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DerelictOutsideCutscene : MonoBehaviour
{
    [SerializeField] public GameObject adultPlayer;

    private Player_Controller _pc;

    // Start is called before the first frame update
    void Start()
    {
        _pc = adultPlayer.GetComponent<Player_Controller>();
        StartCoroutine(StartingWait());
    }

    private IEnumerator StartingWait()
    {
        _pc.enabled = false;
        yield return new WaitForSeconds(11.5f);
        _pc.enabled = true;
    }
}
