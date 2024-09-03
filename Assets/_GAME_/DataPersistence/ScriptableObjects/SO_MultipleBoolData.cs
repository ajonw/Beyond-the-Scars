using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_MultipleBoolData : ScriptableObject
{
    [SerializeField] public SO_BoolData[] SO_BoolDatas;
}
