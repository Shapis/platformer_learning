using System;
using UnityEngine;

public class GemGrabber : MonoBehaviour, ILevelEndsEvents, IScoreKeeperEvents
{
    [SerializeField] private ScoreKeeper m_ScoreKeeper;
    public event EventHandler OnLevelEndsEvent;

    private int totalScore;

    private void Start()
    {
        m_ScoreKeeper.OnScoreUpdateEvent += OnScoreUpdate;
    }

    public void LevelHasBeenCompleted()
    {
        LevelCompletionInfo myLevelCompletionInfo = new LevelCompletionInfo();
        myLevelCompletionInfo = myLevelCompletionInfo.Load();
        myLevelCompletionInfo.AddLevelCompletionInfo(SceneHandler.GetActiveSceneEnum(), totalScore, Time.timeSinceLevelLoad);
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

    public void OnScoreUpdate(object sender, int totalScore)
    {
        this.totalScore = totalScore;
    }
}