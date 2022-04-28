using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioHandlerSettings : BaseSettings, ISceneHandlerEvents
{

    [Header("Dependencies")]
    [SerializeField] private AudioSource m_MusicSource;
    private float m_CurrentMusicVolume;
    private float m_CurrentSfxVolume;

    public float CurrentMusicVolume { get => m_CurrentMusicVolume; private set => m_CurrentMusicVolume = value; }
    public float CurrentSfxVolume { get => m_CurrentSfxVolume; private set => m_CurrentSfxVolume = value; }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad; // subscribe to scene load event, Only needs to be done once since SceneHandler is static and AudioHandler never gets destroyed.
        InitStart();
    }

    public void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0 == SceneManager.GetSceneByName("MainMenu"))
        {
            InitAwake();
            InitStart();
        }
    }

    private void UpdateVolumeSettings(GameSettings.GameSettingsInfo gameSettingsInfo)
    {
        m_MusicSource.volume = gameSettingsInfo.volumeMusic;
        CurrentMusicVolume = gameSettingsInfo.volumeMusic;
        CurrentSfxVolume = gameSettingsInfo.volumeSfx;
    }
    public override void OnGameSettingsInitialized(object sender, GameSettings.GameSettingsInfo gameSettingsInfo)
    {
        m_MusicSource.loop = true;
        UpdateVolumeSettings(gameSettingsInfo);
    }

    public override void OnGameSettingsChanged(object sender, GameSettings.GameSettingsInfo gameSettingsInfo)
    {
        UpdateVolumeSettings(gameSettingsInfo);
    }


}