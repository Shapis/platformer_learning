using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button m_PlayButton;
    [SerializeField] Button m_ResetButton;
    [SerializeField] Button m_QuitButton;



    private void Start()
    {
        m_PlayButton.onClick.AddListener(() => { SceneHandler.Load(SceneHandler.Scene.OverworldMap); });
        m_ResetButton.onClick.AddListener(() => { PlayerPrefs.DeleteAll(); });
        m_QuitButton.onClick.AddListener(() => { Application.Quit(); Debug.Log("Quit application!"); });
    }
}
