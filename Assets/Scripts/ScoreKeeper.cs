using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour, IKeyGrabberEvents, IKeyDoorEvents, IScoreKeeperEvents
{
    [SerializeField] private KeyGrabber m_KeyGrabber;

    public EventHandler<int> OnScoreUpdateEvent;

    private int doorScore = 0;
    private int keyListScore = 0;

    private void Start()
    {
        m_KeyGrabber.OnKeysChangedEvent += OnKeysChanged;

        var myDoors = GameObject.FindObjectsOfType<KeyDoor>();

        foreach (var o in myDoors)
        {
            o.OnDoorOpenEvent += OnDoorOpen;
            o.OnDoorCloseEvent += OnDoorClose;
            o.OnDoorOpenPermanentlyEvent += OnDoorOpenPermanently;
        }
    }

    public void OnDoorClose(object sender, EventArgs e)
    {
        //throw new NotImplementedException();
        //UpdateScore();
    }

    public void OnDoorOpen(object sender, EventArgs e)
    {
        //throw new NotImplementedException();
        //UpdateScore();
    }

    public void OnDoorOpenPermanently(object sender, EventArgs e)
    {
        doorScore += 6;
        UpdateScore();
    }

    public void OnKeysChanged(object sender, List<ColorPalette.ColorName> keyList)
    {
        keyListScore = keyList.Count * 3;
        UpdateScore();
    }

    private void UpdateScore()
    {
        OnScoreUpdate(this, doorScore + keyListScore);
    }

    public void OnScoreUpdate(object sender, int totalScore)
    {
        OnScoreUpdateEvent?.Invoke(this, totalScore);
    }
}
