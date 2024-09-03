using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatSpeed;
    public bool hasStarted;

    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        beatSpeed /= 60f;
        originalPosition = transform.position;
    }

    public void StartGame()
    {
        hasStarted = true;
    }
    public void StopGame()
    {
        hasStarted = false;
        transform.position = originalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            transform.position -= new Vector3(0f, beatSpeed * Time.deltaTime, 0f);
        }
    }
}
