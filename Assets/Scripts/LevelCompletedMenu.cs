using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedMenu : MonoBehaviour, ILevelEndsEvents, IScoreKeeperEvents
{
    [Header("Dependencies")]
    [SerializeField] private PopupMenuController m_LevelCompletedMenuPopUp;
    [SerializeField] TextMeshProUGUI m_FinalScore;
    [SerializeField] TextMeshProUGUI m_FinalTime;
    [SerializeField] Button m_QuitToWorldMap;
    private InputHandler m_InputHandler;
    private GameObject m_Player;

    private int totalScore;

    private bool levelHasEnded = false;

    private void Awake()
    {
        m_InputHandler = FindObjectOfType<InputHandler>();
        m_Player = GameObject.Find("Player");
    }

    private void Start()
    {
        m_InputHandler.OnCancelPressedEvent += (sender, eventArgsEmpty) =>
       {
           if (levelHasEnded)
           {
               SceneHandler.Load(SceneHandler.Scene.OverworldMap);
           };
       };
        m_QuitToWorldMap.onClick.AddListener(() => SceneHandler.Load(SceneHandler.Scene.OverworldMap));
        m_Player.GetComponent<GemGrabber>().OnLevelEndsEvent += OnLevelEnds;
        m_Player.GetComponent<ScoreKeeper>().OnScoreUpdateEvent += OnScoreUpdate;
    }

    public void OnLevelEnds(object sender, EventArgs e)
    {
        m_LevelCompletedMenuPopUp.OpenMenu();
        m_FinalTime.text = String.Format("{0:0.00}", Time.timeSinceLevelLoad);
        m_FinalScore.text = totalScore.ToString();
        GameHandler.Pause();
        // levelHasEnded = true;
    }

    public void OnScoreUpdate(object sender, int totalScore)
    {
        this.totalScore = totalScore;
    }
}