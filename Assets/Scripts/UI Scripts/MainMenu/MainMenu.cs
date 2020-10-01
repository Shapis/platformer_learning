using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Button m_PlayButton;
    [SerializeField] private Button m_OptionsButton;
    [SerializeField] private Button m_QuitButton;
    [SerializeField] private InputHandler m_InputHandler;
    [SerializeField] private PopupMenuController m_PanelOptions;

    private void Start()
    {
        m_InputHandler.OnCancelPressedEvent += CloseOptionsMenu;
        m_PlayButton.onClick.AddListener(() => { SceneHandler.Load(SceneHandler.Scene.OverworldMap); });
        m_OptionsButton.onClick.AddListener(OpenOptionsMenu);
        m_QuitButton.onClick.AddListener(() => { Application.Quit(); Debug.Log("Quit application!"); });
        m_InputHandler.OnMouseButtonLeftPressedEvent += OnMouseButtonLeftPressed;
        // m_LeftBackButton.GetComponent<Button>().onClick.AddListener(() => CloseOptionsMenu(this, EventArgs.Empty));
        // m_RightBackButton.GetComponent<Button>().onClick.AddListener(() => CloseOptionsMenu(this, EventArgs.Empty));
    }

    private void OnMouseButtonLeftPressed(object sender, Vector2 e)
    {
        if (m_PanelOptions.gameObject.activeSelf && !new GeometryHandler().IsTouchInsideObject(e, m_PanelOptions.gameObject))
        {
            m_PanelOptions.CloseMenu();
        }
    }

    private void CloseOptionsMenu(object sender, EventArgs e)
    {
        if (m_PanelOptions.gameObject.activeSelf)
        {
            m_PanelOptions.CloseMenu();
        }
        // if (m_LeftBackButton.activeSelf)
        // {
        //     m_LeftBackButton.GetComponent<PopupMenuController>().CloseMenu();
        // }
        // if (m_RightBackButton.gameObject.activeSelf)
        // {
        //     m_RightBackButton.GetComponent<PopupMenuController>().CloseMenu();
        // }
    }


    private void OpenOptionsMenu()
    {
        m_PanelOptions.OpenMenu();
        // m_LeftBackButton.GetComponent<PopupMenuController>().OpenMenu();
        // m_RightBackButton.GetComponent<PopupMenuController>().OpenMenu();
    }
}
