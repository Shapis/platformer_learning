using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioHandlerSettings : BaseSettings
{

    [Header("Dependencies")]
    [SerializeField] AudioSource m_MusicSource;
    [SerializeField] AudioSource m_SfxSource;

    private void Start()
    {
        SceneHandler.OnSceneLoadEvent(OnSceneLoad); // subscribe to scene load event, Only needs to be done once since SceneHandler is static and AudioHandler never gets destroyed.
        InitStart();
    }

    private void OnSceneLoad(SceneHandler.Scene obj)
    {
        if (obj == SceneHandler.Scene.MainMenu)
        {
            InitAwake();
            InitStart();
        }
    }

    private void UpdateVolumeSettings(GameSettings.GameSettingsInfo gameSettingsInfo)
    {
        m_MusicSource.volume = gameSettingsInfo.volumeMusic;
        m_SfxSource.volume = gameSettingsInfo.volumeSfx;
    }
    public override void OnGameSettingsInitialized(object sender, GameSettings.GameSettingsInfo gameSettingsInfo)
    {
        m_MusicSource.loop = true;
        m_SfxSource.loop = false;
        UpdateVolumeSettings(gameSettingsInfo);
    }

    public override void OnGameSettingsChanged(object sender, GameSettings.GameSettingsInfo gameSettingsInfo)
    {
        UpdateVolumeSettings(gameSettingsInfo);
    }
}