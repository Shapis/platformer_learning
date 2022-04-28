using System;
using UnityEngine;

public abstract class BaseAudioEmitter : MonoBehaviour, IGameHandlerEvents
{
    private int m_Priority = 128;

    private AudioSource m_AudioSource;
    private AudioSource m_PermanentAudioSource; // This audio source is attached to the AudioHandler object, so it persists through scenes, and is set to play even if the game is paused.
    private AudioHandler m_AudioHandler;
    protected AudioClipCatalog m_AudioClipCatalog;
    private AudioHandlerSettings m_AudioHandlerSettings;

    // This runs every time the game reaches the main menu screen
    private void Awake()
    {
        m_AudioHandler = FindObjectOfType<AudioHandler>(); // There should only ever be one AudioHandler.cs in the scene. Currently I have it set so it's started on the MainMenu scene and persists ("DontDestroyOnLoad") through every scene. This is done so the music which is contained inside the AudioHandler gameObject doesn't stop when switching scenes.
        m_PermanentAudioSource = m_AudioHandler.SfxSource;
        m_AudioClipCatalog = FindObjectOfType<AudioClipCatalog>(); // Same as above.
        m_AudioHandlerSettings = FindObjectOfType<AudioHandlerSettings>(); // Same as above.
        m_AudioSource = gameObject.AddComponent<AudioSource>(); // Adds the audiosource that we will be using in our PlaySfx method.
        GameHandler.OnGamePauseEvent += OnGamePause;
        GameHandler.OnGameResumeEvent += OnGameResume;
        InitAwake();

    }

    // This runs once when the game first reaches the main menu screen.
    private void Start()
    {
        InitStart();
    }

    public abstract void InitAwake();
    public abstract void InitStart();


    // Start(); On the start method, you should subscribe in the child class to the events that you want to listen to.

    // You should inherit on the child class to Event Interfaces you want to listen to and implement PlaySfx on them with the specific SfxName you want to play.


    public virtual void PlaySfx(AudioClipCatalog.SfxName sfxName, float relativeVolume = 1f, bool loop = false, float pitch = 1f, float maxDistance = 5f)
    {
        m_AudioSource.clip = m_AudioClipCatalog.GetSfxClip(sfxName);
        m_AudioSource.loop = loop;
        m_AudioSource.priority = m_Priority;
        m_AudioSource.playOnAwake = false;
        m_AudioSource.pitch = pitch;
        m_AudioSource.volume = relativeVolume * m_AudioHandlerSettings.CurrentSfxVolume;
        m_AudioSource.spatialBlend = 1f;
        m_AudioSource.minDistance =
        Math.Abs(FindObjectOfType<AudioListener>().transform.position.z);
        m_AudioSource.maxDistance = m_AudioSource.minDistance + maxDistance;
        m_AudioSource.rolloffMode = AudioRolloffMode.Linear;
        if (m_AudioSource.isPlaying == true)
        {
            m_AudioSource.Stop();
        }
        m_AudioSource.Play(); // The way I have this implemented, only one sound can be played at a time from each AudioEmitter, this is intentional, ex. I don't want the same sound of jumping and landing playing at the same time.
    }

    public virtual void PlaySfxPermanent(AudioClipCatalog.SfxName sfxName, float relativeVolume = 1f, bool loop = false, float pitch = 1f, float maxDistance = 5f)
    {
        m_PermanentAudioSource.clip = m_AudioClipCatalog.GetSfxClip(sfxName);
        m_PermanentAudioSource.loop = loop;
        m_PermanentAudioSource.priority = m_Priority;
        m_PermanentAudioSource.playOnAwake = false;
        m_PermanentAudioSource.pitch = pitch;
        m_PermanentAudioSource.volume = relativeVolume * m_AudioHandlerSettings.CurrentSfxVolume;
        m_PermanentAudioSource.spatialBlend = 1f;
        m_PermanentAudioSource.minDistance =
        Math.Abs(FindObjectOfType<AudioListener>().transform.position.z);
        m_PermanentAudioSource.maxDistance = m_PermanentAudioSource.minDistance + maxDistance;
        m_PermanentAudioSource.rolloffMode = AudioRolloffMode.Linear;
        if (m_PermanentAudioSource.isPlaying == true)
        {
            m_PermanentAudioSource.Stop();
        }
        m_PermanentAudioSource.Play(); // The way I have this implemented, only one sound can be played at a time from each AudioEmitter, this is intentional, ex. I don't want the same sound of jumping and landing playing at the same time.
    }

    public virtual void StopSfx()
    {
        m_AudioSource.Stop();
    }


    // The null checks in these methods are because the AudioHandler is not always present in the scene. The GameHandler persists through scenes. So if you pause the game, the audiosource might be destroyed in a change of scenes, but the gamehandler will keep listening to it.
    public void OnGamePause(object sender, EventArgs e)
    {
        if (m_AudioSource != null)
        {
            m_AudioSource.Pause();
        }
    }

    public void OnGameResume(object sender, EventArgs e)
    {
        if (m_AudioSource != null)
        {
            m_AudioSource.UnPause();
        }
        // m_AudioSource?.UnPause();
    }
}