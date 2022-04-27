using System;
using UnityEngine;

public abstract class BaseAudioEmitter : MonoBehaviour, IGameHandlerEvents
{
    private int m_Priority = 128;
    private float m_SpatialBlend = 1f;
    private AudioSource m_AudioSource;
    private AudioHandler m_AudioHandler;
    protected AudioClipCatalog m_AudioClipCatalog;
    private AudioHandlerSettings m_AudioHandlerSettings;

    private void Awake()
    {
        m_AudioHandler = FindObjectOfType<AudioHandler>(); // There should only ever be one AudioHandler.cs in the scene. Currently I have it set so it's started on the MainMenu scene and persists ("DontDestroyOnLoad") through every scene. This is done so the music which is contained inside the AudioHandler gameObject doesn't stop when switching scenes.
        m_AudioClipCatalog = FindObjectOfType<AudioClipCatalog>(); // Same as above.
        m_AudioHandlerSettings = FindObjectOfType<AudioHandlerSettings>(); // Same as above.
        m_AudioSource = gameObject.AddComponent<AudioSource>(); // Adds the audiosource that we will be using in our PlaySfx method.
        GameHandler.OnGamePauseEvent += OnGamePause;
        InitAwake();
    }

    private void Start()
    {
        InitStart();
    }

    public abstract void InitAwake(); // Find your gameObjects in this method.
    public abstract void InitStart(); // Subscribe to your events in this method.


    // Start(); On the start method, you should subscribe in the child class to the events that you want to listen to.

    // You should inherit on the child class to Event Interfaces you want to listen to and implement PlaySfx on them with the specific SfxName you want to play.


    public virtual void PlaySfx(AudioClipCatalog.SfxName sfxName, float relativeVolume = 1f, bool loop = false)
    {
        m_AudioSource.clip = m_AudioClipCatalog.GetSfxClip(sfxName);
        m_AudioSource.loop = loop;
        m_AudioSource.priority = m_Priority;
        m_AudioSource.playOnAwake = false;
        m_AudioSource.volume = relativeVolume * m_AudioHandlerSettings.CurrentSfxVolume;
        if (m_AudioSource.isPlaying == true)
        {
            m_AudioSource.Stop();
        }
        m_AudioSource.Play(); // The way I have this implemented, only one sound can be played at a time from each AudioEmitter, this is intentional, ex. I don't want the same sound of jumping and landing at the same time.
    }

    public virtual void StopSfx()
    {
        m_AudioSource.Stop();
    }

    public void OnGamePause(object sender, EventArgs e)
    {
        m_AudioSource.Pause();
    }

    public void OnGameResume(object sender, EventArgs e)
    {
        m_AudioSource.UnPause();
    }
}