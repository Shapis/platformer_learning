using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedMenu : MonoBehaviour {
    [SerializeField] private InputHandler m_InputHandler;

    [SerializeField] private PopupMenuController m_LevelCompletedMenuPopUp;

    [SerializeField] private GameObject m_Player;

    [SerializeField] TextMeshProUGUI m_FinalScore;

    [SerializeField] TextMeshProUGUI m_FinalTime;

    [SerializeField] Button m_QuitToWorldMap;

    private bool levelHasEnded = false;

    private void Awake () {
        m_InputHandler.OnCancelPressedEvent += (a, b) => {
            if (levelHasEnded) {
                SceneHandler.Load (SceneHandler.Scene.OverworldMap);
            };
        };
        m_QuitToWorldMap.onClick.AddListener (ReturnToWorldMap);
    }

    private void Start () {
        m_Player.GetComponent<GemGrabber> ().OnLevelEndsEvent += OnLevelEnd;
    }

    private void OnLevelEnd (object sender, EventArgs e) {
        m_LevelCompletedMenuPopUp.OpenMenu ();
        GameHandler.Pause ();
        m_FinalTime.text = String.Format ("{0:0.00}", m_Player.GetComponent<PlayerScoreKeeper> ().FinalTotalTime);
        m_FinalScore.text = m_Player.GetComponent<PlayerScoreKeeper> ().TotalScore.ToString ();
        levelHasEnded = true;
    }

    private void ReturnToWorldMap () {
        SceneHandler.Load (SceneHandler.Scene.OverworldMap);
    }
}