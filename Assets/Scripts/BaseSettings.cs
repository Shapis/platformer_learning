using System;
using UnityEngine;

public abstract class BaseSettings : MonoBehaviour, IGameSettingsEvents
{
    private GameSettings m_GameSettings;
    private GameSettings.GameSettingsInfo gameSettingsInfo = new GameSettings.GameSettingsInfo();

    private void Awake()
    {
        InitAwake(); // This exists so you can call awake from a child class that doesn't get destroyed on load.

    }

    protected void InitAwake()
    {
        m_GameSettings = GameObject.FindObjectOfType<GameSettings>();
    }

    private void Start()
    {
        InitStart(); // This exists so you can call start from a child class that doesn't get destroyed on load.
    }

    protected void InitStart()
    {
        try
        {
            gameSettingsInfo = SaveHandler<GameSettings.GameSettingsInfo>.Load(SaveHandler<GameSettings.GameSettingsInfo>.SaveFileName.gameSettingsInfo);
            OnGameSettingsInitialized(this, gameSettingsInfo);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("No game settings info file has been found, this should only ever happen in the first initialization, and only in the main menu scene");
        }
        if (m_GameSettings != null)
        {
            m_GameSettings.OnGameSettingsChangedEvent += OnGameSettingsChanged;
        }
    }

    // This method will be called once the first time the scene is initialized during the Start() method.
    public abstract void OnGameSettingsInitialized(object sender, GameSettings.GameSettingsInfo gameSettingsInfo);

    // This method will be called every time there's a change to the GameSettings
    public abstract void OnGameSettingsChanged(object sender, GameSettings.GameSettingsInfo gameSettingsInfo);
}
