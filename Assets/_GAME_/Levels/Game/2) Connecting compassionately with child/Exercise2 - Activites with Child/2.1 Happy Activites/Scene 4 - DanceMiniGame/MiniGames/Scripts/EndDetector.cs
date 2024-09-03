using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDetector : MonoBehaviour
{
    [SerializeField] public MiniGameManager miniGameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            miniGameManager.EndGame();
        }
    }
}
