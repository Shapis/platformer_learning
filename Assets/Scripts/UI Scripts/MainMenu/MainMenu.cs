using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_PlayButton;
    [SerializeField] private Button m_OptionsButton;
    [SerializeField] private Button m_QuitButton;
    [SerializeField] private InputHandler m_InputHandler;

    [SerializeField] private PopupMenuController m_PanelOptions;

    [SerializeField] private Button m_BackgroundBackButton;

    [SerializeField] private Button m_PanelOptionsReset;

    [SerializeField] private GameObject m_LeftBackButton;

    [SerializeField] private GameObject m_RightBackButton;

    private void Start()
    {
        m_InputHandler.OnCancelPressedEvent += CloseOptionsMenu;
        m_PlayButton.onClick.AddListener(() => { SceneHandler.Load(SceneHandler.Scene.OverworldMap); });
        m_OptionsButton.onClick.AddListener(OpenOptionsMenu);
        m_QuitButton.onClick.AddListener(() => { Application.Quit(); Debug.Log("Quit application!"); });
        m_BackgroundBackButton.onClick.AddListener(() => { CloseOptionsMenu(this, EventArgs.Empty); });
        m_PanelOptionsReset.onClick.AddListener(() => { ResetPlayerPrefs(this, EventArgs.Empty); CloseOptionsMenu(this, EventArgs.Empty); });
        m_LeftBackButton.GetComponent<Button>().onClick.AddListener(() => CloseOptionsMenu(this, EventArgs.Empty));
        m_RightBackButton.GetComponent<Button>().onClick.AddListener(() => CloseOptionsMenu(this, EventArgs.Empty));
    }

    private void ResetPlayerPrefs(object sender, EventArgs e)
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Reset PlayerPrefs! With Cancel Button!");
    }

    private void CloseOptionsMenu(object sender, EventArgs e)
    {
        m_PanelOptions.CloseMenu();
        m_LeftBackButton.GetComponent<PopupMenuController>().CloseMenu();
        m_RightBackButton.GetComponent<PopupMenuController>().CloseMenu();
    }


    private void OpenOptionsMenu()
    {
        m_PanelOptions.OpenMenu();
        m_LeftBackButton.GetComponent<PopupMenuController>().OpenMenu();
        m_RightBackButton.GetComponent<PopupMenuController>().OpenMenu();
    }
}
