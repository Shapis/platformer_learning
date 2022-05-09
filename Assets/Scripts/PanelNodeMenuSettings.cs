
using UnityEngine;

public class PanelNodeMenuSettings : BaseSettings
{
    [Header("Dependencies")]
    [SerializeField] private Transform m_PanelNodeMenu;

    [Header("Settings")]
    [SerializeField] private float m_HorizontalSpacing = 175f;
    [SerializeField] private float m_VerticalSpacing = 145f;

    public override void OnGameSettingsInitialized(object sender, GameSettings.GameSettingsInfo gameSettingsInfo)
    {
        MovePanelAnchor(gameSettingsInfo);
    }
    public override void OnGameSettingsChanged(object sender, GameSettings.GameSettingsInfo gameSettingsInfo)
    {
        MovePanelAnchor(gameSettingsInfo);
    }

    private void MovePanelAnchor(GameSettings.GameSettingsInfo gameSettingsInfo)
    {
        RectTransform m_RectTransform = m_PanelNodeMenu.GetComponent<RectTransform>();
        //m_RectTransform.anchoredPosition = new Vector2(-100f, -200f);

        switch (gameSettingsInfo.rightHandedMode)
        {
            case true:
                m_RectTransform.anchoredPosition = new Vector2(m_HorizontalSpacing, -m_VerticalSpacing);
                m_RectTransform.anchorMax = new Vector2(0f, 1f);
                m_RectTransform.anchorMin = new Vector2(0f, 1f);
                m_RectTransform.pivot = new Vector2(0f, 1f);
                break;
            case false:
                m_RectTransform.anchoredPosition = new Vector2(-m_HorizontalSpacing, -m_VerticalSpacing);
                m_RectTransform.anchorMax = new Vector2(1f, 1f);
                m_RectTransform.anchorMin = new Vector2(1f, 1f);
                m_RectTransform.pivot = new Vector2(1f, 1f);
                break;
        }
    }

    protected override void InitAwake()
    {
        //throw new System.NotImplementedException();
    }

    protected override void InitStart()
    {
        //throw new System.NotImplementedException();
    }
}
