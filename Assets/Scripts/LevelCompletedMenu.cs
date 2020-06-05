using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedMenu : MonoBehaviour, ILevelEndsEvents
{
    [Header("Dependencies")]
    [SerializeField] private PopupMenuController m_LevelCompletedMenuPopUp;
    [SerializeField] TextMeshProUGUI m_FinalScore;
    [SerializeField] TextMeshProUGUI m_FinalTime;
    [SerializeField] Button m_QuitToWorldMap;
    private InputHandler m_InputHandler;
    private GameObject m_Player;

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
    }

    public void OnLevelEnds(object sender, EventArgs e)
    {
        m_LevelCompletedMenuPopUp.OpenMenu();
        GameHandler.Pause();
        //m_FinalTime.text = String.Format("{0:0.00}", m_Player.GetComponent<PlayerScoreKeeper>().FinalTotalTime);
        //m_FinalScore.text = m_Player.GetComponent<PlayerScoreKeeper>().TotalScore.ToString();
        // levelHasEnded = true;
    }
}