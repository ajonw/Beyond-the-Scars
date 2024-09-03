using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_SongEntry : ScriptableObject
{
    [SerializeField] public AudioClip song;

    [TextArea][SerializeField] public string[] lyrics;
    [SerializeField] public float[] timestamps;


}
