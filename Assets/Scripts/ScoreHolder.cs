using System;
using TMPro;
using UnityEngine;

public class ScoreHolder : MonoBehaviour, IScoreKeeperEvents, ILevelEndsEvents
{
    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI m_CurrentScore;
    [SerializeField] private TextMeshProUGUI m_CurrentTime;

    private bool myLevelEndsSwitch = false;

    private void Start()
    {
        GameObject.Find("Player").GetComponent<ScoreKeeper>().OnScoreUpdateEvent += OnScoreUpdate;
        GameObject.Find("Player").GetComponent<GemGrabber>().OnLevelEndsEvent += OnLevelEnds;
        OnScoreUpdate(this, 0); // Initializing and refreshing the scoreboard as 0.
    }

    public void OnScoreUpdate(object sender, int totalScore)
    {
        m_CurrentScore.text = totalScore.ToString();
    }

    private void Update()
    {
        if (!myLevelEndsSwitch)
        {
            m_CurrentTime.text = String.Format("{0:0.00}", Time.timeSinceLevelLoad);
        }
    }

    public void OnLevelEnds(object sender, EventArgs e)
    {
        myLevelEndsSwitch = true;
        m_CurrentTime.text = String.Format("{0:0.00}", Time.timeSinceLevelLoad);
    }
}
