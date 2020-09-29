using System;
using TMPro;
using UnityEngine;

public class ScoreHolder : MonoBehaviour, IScoreKeeperEvents, ILevelEndsEvents
{
    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI m_CurrentScore;
    [SerializeField] private RectTransform m_PanelScore;
    [SerializeField] private TextMeshProUGUI m_CurrentTime;
    [SerializeField] private RectTransform m_PanelTime;
    private Vector2 initialScoreHolderSize;
    private Vector2 initialTimeHolderSize;
    private readonly TimeFormatHandler timeFormatter = new TimeFormatHandler();

    private bool myLevelEndsSwitch = false;

    private void Start()
    {
        GameObject.Find("Player").GetComponent<ScoreKeeper>().OnScoreUpdateEvent += OnScoreUpdate;
        GameObject.Find("Player").GetComponent<GemGrabber>().OnLevelEndsEvent += OnLevelEnds;
        initialScoreHolderSize = new Vector2(m_PanelScore.rect.width, m_PanelScore.rect.height);
        initialTimeHolderSize = new Vector2(m_PanelTime.rect.width, m_PanelTime.rect.height);
        OnScoreUpdate(this, 500); // Initializing and refreshing the scoreboard as 0.
    }

    public void OnScoreUpdate(object sender, int totalScore)
    {
        m_CurrentScore.text = "SCORE: " + totalScore.ToString();
        m_PanelScore.sizeDelta = new Vector2(ResizeHolder(m_CurrentScore.text.Length, initialScoreHolderSize.x), initialScoreHolderSize.y);
        //Debug.Log(initialScoreHolderSize);
    }

    // TODO: This needs to adapt to the size of the font. The font isnt monospaced so depending on the numbers it loses its alignment.
    private float ResizeHolder(int length, float initialSize)
    {
        return ((initialSize - 8.8f) + (8.8f * length));
    }

    private void Update()
    {
        if (!myLevelEndsSwitch)
        {
            m_CurrentTime.text = timeFormatter.FormatTime(Time.timeSinceLevelLoad);
            m_PanelTime.sizeDelta = new Vector2(ResizeHolder(m_CurrentTime.text.Length, initialTimeHolderSize.x), initialTimeHolderSize.y);
        }
    }

    public void OnLevelEnds(object sender, EventArgs e)
    {
        myLevelEndsSwitch = true;
        m_CurrentTime.text = String.Format("{0:0.00}", Time.timeSinceLevelLoad);
    }
}
