using System;
using UnityEngine;
using UnityEngine.UI;
public class GameSettings : MonoBehaviour, IGameSettingsEvents
{
    [Header("Dependencies")]
    [SerializeField] private Slider m_MusicVolumeSlider;
    [SerializeField] private Slider m_SfxVolumeSlider;
    [SerializeField] private Slider m_RightHandedSlider;
    [SerializeField] private Slider m_JoystickSizeSlider;
    [SerializeField] private Button m_ResetButton;
    public EventHandler<GameSettingsInfo> OnGameSettingsChangedEvent;
    private GameSettingsInfo myGameSettingsInfoDefault = new GameSettingsInfo { rightHandedMode = true, joystickSize = 0.5f, volumeMusic = 0.75f, volumeSfx = 0.75f };
    private GameSettingsInfo myGameSettingsInfo = new GameSettingsInfo();

    private void Awake()
    {
        myGameSettingsInfo = myGameSettingsInfoDefault;

        // Attempts to load the game settings from file, if no file exists, loads the default ones and creates a save file.
        try
        {
            myGameSettingsInfo = SaveHandler<GameSettingsInfo>.Load(SaveHandler<GameSettingsInfo>.SaveFileName.gameSettingsInfo);
            LoadGameSettings(myGameSettingsInfo);
            //OnGameSettingsChanged(this, myGameSettingsInfo);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("No game settings info file has been found, initializing and creating one now");
            LoadGameSettings(myGameSettingsInfo);
            SaveInfo();
            //OnGameSettingsChanged(this, myGameSettingsInfo);
        }
    }

    private void Start()
    {

        m_MusicVolumeSlider.onValueChanged.AddListener(i => VolumeMusicChanged(i));
        m_SfxVolumeSlider.onValueChanged.AddListener(i => SfxVolumeChanged(i));
        m_RightHandedSlider.onValueChanged.AddListener(i => RightHandedModeChanged(i));
        m_JoystickSizeSlider.onValueChanged.AddListener(i => JoystickSizeChanged(i));
        m_ResetButton.onClick.AddListener(() => ResetPlayerPrefs());
    }

    private void VolumeMusicChanged(float volume)
    {
        myGameSettingsInfo.volumeMusic = volume;
        SaveInfo();
        OnGameSettingsChanged(this, myGameSettingsInfo);
    }

    private void SfxVolumeChanged(float volume)
    {
        myGameSettingsInfo.volumeSfx = volume;
        SaveInfo();
        OnGameSettingsChanged(this, myGameSettingsInfo);
    }

    private void SaveInfo()
    {
        SaveHandler<GameSettingsInfo>.Save(myGameSettingsInfo, SaveHandler<GameSettingsInfo>.SaveFileName.gameSettingsInfo);
    }

    private void RightHandedModeChanged(float rightHandedMode)
    {
        switch (rightHandedMode)
        {
            case 0: myGameSettingsInfo.rightHandedMode = false; break;
            case 1: myGameSettingsInfo.rightHandedMode = true; break;
        }
        SaveInfo();
        OnGameSettingsChanged(this, myGameSettingsInfo);
    }

    private void JoystickSizeChanged(float joystickSize)
    {
        myGameSettingsInfo.joystickSize = joystickSize;
        SaveInfo();
        OnGameSettingsChanged(this, myGameSettingsInfo);
    }

    private void LoadGameSettings(GameSettingsInfo gameSettingsInfo)
    {
        m_MusicVolumeSlider.value = gameSettingsInfo.volumeMusic;
        m_SfxVolumeSlider.value = gameSettingsInfo.volumeSfx;
        m_JoystickSizeSlider.value = gameSettingsInfo.joystickSize;
        switch (gameSettingsInfo.rightHandedMode)
        {
            case false: m_RightHandedSlider.value = 0f; break;
            case true: m_RightHandedSlider.value = 1f; break;
        }
    }

    public void OnGameSettingsChanged(object sender, GameSettingsInfo gameSettingsInfo)
    {
        OnGameSettingsChangedEvent?.Invoke(this, gameSettingsInfo);
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Reset PlayerPrefs! With Reset Button!");
        myGameSettingsInfo = myGameSettingsInfoDefault;
        LoadGameSettings(myGameSettingsInfo);
        OnGameSettingsChanged(this, myGameSettingsInfo);
        gameObject.GetComponent<PopupMenuController>().CloseMenu();
    }

    [System.Serializable]
    public struct GameSettingsInfo
    {
        public bool rightHandedMode;
        public float joystickSize;
        public float volumeMusic;
        public float volumeSfx;
    }
}
