using UnityEngine;
using System.Linq;
using System;

public class LevelCompletion
{
    public void LevelHasBeenCompleted()
    {
        LevelCompletionInfo myLevelCompletionInfo = new LevelCompletionInfo();
        myLevelCompletionInfo = myLevelCompletionInfo.Load();
        myLevelCompletionInfo.AddLevelCompletionInfo(SceneHandler.GetActiveSceneEnum(),
        GameObject.Find("Player").GetComponent<PlayerScoreKeeper>().TotalScore,
        GameObject.Find("Player").GetComponent<PlayerScoreKeeper>().TotalTime);
        GameHandler.Pause();
        return;
    }
}
