using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MobileJoystickSettings : BaseSettings
{
    [Header("Dependencies")]
    private Image[] m_JoystickSprites;

    [Header("Settings")]
    private float defaultScale = 6f;
    [SerializeField] private bool m_InvisibleByDefault = false;
    private Coroutine fade = null;
    private float hasChangedRecentlyTimer = 0f;

    protected override void InitAwake()
    {
        //Check if the device running this is a desktop
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            this.gameObject.SetActive(false);
        }
    }

    protected override void InitStart()
    {
        //throw new System.NotImplementedException();
    }

    private void InititalizeSprites()
    {
        m_JoystickSprites = GetComponentsInChildren<Image>();

        if (m_InvisibleByDefault)
        {
            foreach (var o in m_JoystickSprites)
            {
                o.color = new Color(o.color.r, o.color.g, o.color.b, 0f);
            }
        }
    }

    public override void OnGameSettingsInitialized(object sender, GameSettings.GameSettingsInfo gameSettingsInfo)
    {
        InititalizeSprites();
        UpdateJoystickSettings(gameSettingsInfo);
    }

    public override void OnGameSettingsChanged(object sender, GameSettings.GameSettingsInfo myGameSettingsInfo)
    {
        hasChangedRecentlyTimer = 0f;
        UpdateJoystickSettings(myGameSettingsInfo);
        if (m_InvisibleByDefault)
        {
            if (fade == null)
            {
                fade = StartCoroutine(FadeIn(0.75f));
            }
        }
    }

    private IEnumerator FadeIn(float seconds)
    {
        float timer = 0f;
        while (m_JoystickSprites[0].color.a < 1f)
        {
            foreach (var o in m_JoystickSprites)
            {
                o.color = new Color(o.color.r, o.color.g, o.color.b, Mathf.Lerp(0f, 1f, timer));
            }
            timer += Time.deltaTime / seconds;
            hasChangedRecentlyTimer += Time.deltaTime / (seconds * 2);
            yield return null;
        }
        while (hasChangedRecentlyTimer <= 1f)
        {
            hasChangedRecentlyTimer += Time.deltaTime / (seconds * 2);
            yield return null;
        }
        StartCoroutine(FadeOut(seconds));
    }

    private IEnumerator FadeOut(float seconds)
    {
        float timer = 0f;
        while (m_JoystickSprites[0].color.a > 0f)
        {
            foreach (var o in m_JoystickSprites)
            {
                o.color = new Color(o.color.r, o.color.g, o.color.b, Mathf.Lerp(1f, 0f, timer));
            }
            timer += Time.deltaTime / seconds;
            yield return null;
        }
        fade = null;
    }

    private void UpdateJoystickSettings(GameSettings.GameSettingsInfo myGameSettingsInfo)
    {
        JoystickSide(myGameSettingsInfo.rightHandedMode);
        transform.localScale = new Vector3(defaultScale + myGameSettingsInfo.joystickSize * 3f, defaultScale + myGameSettingsInfo.joystickSize * 3f, 1f);
    }

    private void JoystickSide(bool rightSide)
    {
        RectTransform m_RectTransform = GetComponent<RectTransform>();
        m_RectTransform.anchoredPosition = new Vector2(-100f, -200f);

        switch (rightSide)
        {
            case true: m_RectTransform.anchoredPosition = new Vector2(-50f, 50f); m_RectTransform.anchorMax = new Vector2(1f, 0f); m_RectTransform.anchorMin = new Vector2(1f, 0f); m_RectTransform.pivot = new Vector2(1f, 0f); break;
            case false: m_RectTransform.anchoredPosition = new Vector2(50f, 50f); m_RectTransform.anchorMax = new Vector2(0f, 0f); m_RectTransform.anchorMin = new Vector2(0f, 0f); m_RectTransform.pivot = new Vector2(0f, 0f); break;
        }
    }
}
