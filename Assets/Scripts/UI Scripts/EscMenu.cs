using System;
using UnityEngine;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour
{
    [SerializeField] private InputHandler m_InputHandler;

    [SerializeField] private Button m_ResumeButton;

    [SerializeField] private Button m_BigBackButton;

    [SerializeField] private Button m_QuitToWorldMap;

    [SerializeField] private GameObject m_Player;

    [SerializeField] private PopupMenuController m_PopUpEscMenuPanel;

    private bool myCancelSwitch = true;

    private bool myLevelHasEnded = false;

    private void Awake()
    {
        m_InputHandler.OnCancelPressedEvent += OnCancelPressed;
        m_BigBackButton.onClick.AddListener(() => OnCancelPressed(this, EventArgs.Empty));
        m_ResumeButton.onClick.AddListener(() => OnCancelPressed(this, EventArgs.Empty));
        m_QuitToWorldMap.onClick.AddListener(() => QuitToWorldMap());
        m_Player.GetComponent<GemGrabber>().OnLevelEndsEvent += OnLevelEnds;
    }

    private void QuitToWorldMap()
    {
        SceneHandler.Load(SceneHandler.Scene.OverworldMap);
    }

    private void OnCancelPressed(object sender, EventArgs e)
    {
        if (!myLevelHasEnded)
        {
            if (myCancelSwitch)
            {
                m_PopUpEscMenuPanel.OpenMenu();
                GameHandler.Pause();
                myCancelSwitch = false;
                m_BigBackButton.gameObject.SetActive(true);
            }
            else
            {
                m_PopUpEscMenuPanel.CloseMenu();
                GameHandler.Resume();
                myCancelSwitch = true;
                m_BigBackButton.gameObject.SetActive(false);
            }
        }
    }

    private void OnLevelEnds(object sender, EventArgs e)
    {
        myLevelHasEnded = true;
    }
}
