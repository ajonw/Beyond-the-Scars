using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[RequireComponent(typeof(Light2D))]
public class WorldLight : MonoBehaviour
{
    private Light2D _light;

    [SerializeField]
    private Gradient _gradient;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
    }

}
