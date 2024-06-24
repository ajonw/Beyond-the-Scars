using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerSelect : MonoBehaviour
{
    [SerializeField] public GameObject finger0;
    [SerializeField] public GameObject finger1;
    [SerializeField] public GameObject finger2;

    private void Start()
    {
        finger0.SetActive(false);
        finger1.SetActive(false);
        finger2.SetActive(false);
    }

    public void ActivateFinger(int id)
    {
        switch (id)
        {
            case 0:
                finger0.SetActive(true);
                break;
            case 1:
                finger1.SetActive(true);
                break;
            case 2:
                finger2.SetActive(true);
                break;
        }
    }

    public void DeactivateFinger(int id)
    {
        switch (id)
        {
            case 0:
                finger0.SetActive(false);
                break;
            case 1:
                finger1.SetActive(false);
                break;
            case 2:
                finger2.SetActive(false);
                break;
        }
    }
}
