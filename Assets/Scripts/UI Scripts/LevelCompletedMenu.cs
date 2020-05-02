using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LevelCompletedMenu : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI m_NewBestScore;

    [SerializeField] TextMeshProUGUI m_OldBestScore;

    [SerializeField] TextMeshProUGUI m_NewTime;

    [SerializeField] TextMeshProUGUI m_OldBestTime;

    [SerializeField] Button m_ContinueButton;

    [SerializeField] Button m_QuitButton;

    private void Start()
    {
        m_ContinueButton.onClick.AddListener(() => { SceneHandler.Load(SceneHandler.Scene.OverworldMap); });
        m_QuitButton.onClick.AddListener(() => { SceneHandler.Load(SceneHandler.Scene.MainMenu); });
    }
}
