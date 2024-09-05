using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public LevelLoader levelLoader;
    public SceneField replayScene;
    public SceneField mainMenu;

    public void Replay(int index)
    {
        RewardSystem.secureAttachmentLevel = 0;
        if (index == 0)
        {
            levelLoader.LoadNextLevel(replayScene);
        }
        else if (index == 1)
        {
            levelLoader.LoadNextLevel(mainMenu);
        }
    }
}
