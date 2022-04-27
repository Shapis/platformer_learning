using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameSettings;

public class AudioHandler : MonoBehaviour, IAudioEvents, INodeMovementEvents, ISceneHandlerEvents

{
    [Header("Dependencies")]
    [SerializeField] AudioClipCatalog m_AudioClipCatalog;
    [SerializeField] AudioSource m_MusicSource;
    [SerializeField] private bool m_DebugLoggingEnabled = false;
    private AudioClipCatalog.MusicName _currentMusic;
    private PlayerNodeMovement m_PlayerNodeMovement;

    public event EventHandler OnMusicPlayEvent;

    public event EventHandler OnSfxPlayEvent;
    public event EventHandler OnSfxPauseEvent;
    public event EventHandler OnSfxStopEvent;
    public event EventHandler onMusicPlayEvent;
    public event EventHandler onMusicPauseEvent;
    public event EventHandler onMusicStopEvent;

    private void Awake()
    {
        DontDestroyOnLoad();
    }

    private void Start()
    {
        InitializeSceneMusic();
        SceneHandler.OnSceneLoadEvent(OnSceneLoad); // subscribe to scene load event, Only needs to be done once since SceneHandler is static and AudioHandler never gets destroyed.
        SubscribeToEvents(); // Needs to be run once at start because the SceneHandler doesnt call OnSceneLoadEvent when the very first scene is loaded. (Only when a scene is explicitly loaded from the SceneHandler)
    }

    private void SubscribeToEvents()
    {
        try
        {
            m_PlayerNodeMovement = FindObjectOfType<PlayerNodeMovement>();
            m_PlayerNodeMovement.OnDestinationNodeReachedEvent += OnDestinationNodeReached;
            m_PlayerNodeMovement.OnDestinationNodeDepartedEvent += OnDestinationNodeDeparted;
            m_PlayerNodeMovement.OnTravelNodeReachedEvent += OnTravelNodeReached;
            m_PlayerNodeMovement.OnTravelNodeDepartedEvent += OnTravelNodeDeparted;
            m_PlayerNodeMovement.OnInitialTravelNodeLoadedEvent += OnInitialDestinationNodeLoaded;
            m_PlayerNodeMovement.OnDestinationNotAccessibleEvent += OnDestinationNotAccessible;
            m_PlayerNodeMovement.OnNoDestinationFoundEvent += OnNoDestinationFound;
            if (m_DebugLoggingEnabled)
            {
                Debug.Log("AudioHandler: PlayerNodeMovement found ");
            }
        }
        catch (Exception e)
        {
            if (m_DebugLoggingEnabled)
            {
                Debug.Log("AudioHandler: PlayerNodeMovement not found " + e.Message);
            }
        }

    }


    private void InitializeSceneMusic()
    {
        m_MusicSource.clip = m_AudioClipCatalog.GetMusicClip(AudioClipCatalog.MusicName.The_Journey_Is_The_Treasure);
        m_MusicSource.Play();
        _currentMusic = AudioClipCatalog.MusicName.The_Journey_Is_The_Treasure;
        OnMusicPlay(this, EventArgs.Empty);
    }

    private void PlayMusic(AudioClipCatalog.MusicName _musicName)
    {
        if (_currentMusic != _musicName)
        {
            if (m_DebugLoggingEnabled)
            {
                Debug.Log("AudioHandler: Now playing: " + _musicName);
            }
            m_MusicSource.Stop();
            m_MusicSource.clip = m_AudioClipCatalog.GetMusicClip(_musicName);
            m_MusicSource.Play();
            _currentMusic = _musicName;
            OnMusicPlay(this, EventArgs.Empty);
        }
    }

    public void OnSceneLoad(SceneHandler.Scene currentScene)
    {
        if (currentScene == SceneHandler.Scene.MainMenu)
        {
            PlayMusic(AudioClipCatalog.MusicName.The_Journey_Is_The_Treasure);
        }
        SubscribeToEvents();
    }

    // This makes sure there's always only one AudioHandler in the scene, and that it's not destroyed when changing scenes.
    private void DontDestroyOnLoad()
    {
        if (FindObjectsOfType<AudioHandler>().Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }


    public void OnSfxPlay(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnSfxStop(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnSfxPause(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
    public void OnMusicStop(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnMusicPause(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnMusicPlay(object sender, EventArgs e)
    {
        OnMusicPlayEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnInitialDestinationNodeLoaded(object sender, GameObject nodeInfo)
    {
        PlayMusic(nodeInfo.GetComponent<Node>().GetMusic());
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("AudioHandler: OnInitialDestinationNodeLoaded: " + nodeInfo); // This is called when the player first enters the scene.
        }
    }

    public void OnTravelNodeReached(object sender, GameObject nodeInfo)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("AudioHandler: OnTravelNodeReached: " + nodeInfo); // This is called when the player reaches a travel node.
        }
    }

    public void OnTravelNodeDeparted(object sender, GameObject nodeInfo)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("AudioHandler: OnTravelNodeDeparted: " + nodeInfo); // This is called when the player departs a travel node.
        }
    }

    public void OnDestinationNodeReached(object sender, GameObject nodeInfo)
    {
        PlayMusic(nodeInfo.GetComponent<Node>().GetMusic());
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("AudioHandler: OnDestinationNodeReached: " + nodeInfo); // This is called when the player reaches a destination node.
        }
    }

    public void OnDestinationNodeDeparted(object sender, GameObject nodeInfo)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("AudioHandler: OnDestinationNodeDeparted: " + nodeInfo); // This is called when the player departs a destination node.
        }
    }

    public void OnDestinationNotAccessible(object sender, GameObject nodeInfo)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("AudioHandler: OnDestinationNotAccessible" + nodeInfo); // This is called when the player tries to travel to a destination node that is not accessible.
        }
    }

    public void OnNoDestinationFound(object sender, string s)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("AudioHandler: OnNoDestinationFound: " + s); // This is called when the player tries to travel to a destination node that is not accessible.
        }
    }
}
