using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] public Cutscene _cutscene;
    [SerializeField] public bool _playCutsceneOnStart;

    private void Start()
    {
        if (_playCutsceneOnStart)
        {
            playCutscene(0);
        }
    }

    public void playCutscene(int cutsceneID)
    {
        _cutscene.play(cutsceneID);
    }
}
