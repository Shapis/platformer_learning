using System;
using UnityEngine;

public class GemGrabber : MonoBehaviour, ILevelEndsEvents
{
    public event EventHandler OnLevelEndsEvent;

    public void LevelHasBeenCompleted()
    {
        LevelCompletionInfo myLevelCompletionInfo = new LevelCompletionInfo();
        myLevelCompletionInfo = myLevelCompletionInfo.Load();
        myLevelCompletionInfo.AddLevelCompletionInfo(SceneHandler.GetActiveSceneEnum(), 2, 2);
        // GameObject.Find("Player").GetComponent<PlayerScoreKeeper>().TotalScore,
        // GameObject.Find("Player").GetComponent<PlayerScoreKeeper>().FinalTotalTime);
        return;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Gem>() != null && other.gameObject.GetComponent<Gem>().Tangible)
        {
            other.gameObject.GetComponent<Gem>().Tangible = false;
            LevelHasBeenCompleted();
            OnLevelEnds(this, EventArgs.Empty);
        }
    }

    public void OnLevelEnds(object sender, EventArgs e)
    {
        OnLevelEndsEvent?.Invoke(this, EventArgs.Empty);
    }
}