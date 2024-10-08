using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballScoreDetector : MonoBehaviour
{
    public BasketBallGameManager gameManager;
    public AudioSource coinSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BasketBall")
        {
            gameManager.UpdateScore();
            coinSound.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {

        }
    }
}
