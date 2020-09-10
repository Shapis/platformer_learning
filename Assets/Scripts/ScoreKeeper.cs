using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour, IKeyGrabberEvents, IKeyDoorEvents, IScoreKeeperEvents, ICoinEvents, IPressurePlateEvents, IChestGrabberEvents
{
    [SerializeField] private KeyGrabber m_KeyGrabber;
    [SerializeField] private CoinGrabber m_CoinGrabber;
    [SerializeField] private ChestGrabber m_ChestGrabber;

    public EventHandler<int> OnScoreUpdateEvent;

    private int doorScore = 0;
    private int keyListScore = 0;
    private int coinListScore = 0;
    private int pressurePlateScore = 0;
    private int chestScore = 0;

    private void Start()
    {
        m_KeyGrabber.OnKeysChangedEvent += OnKeysChanged;
        m_CoinGrabber.OnCoinsChangedEvent += OnCoinsChanged;
        m_ChestGrabber.OnChestOpenedEvent += OnChestOpened;

        var myDoors = GameObject.FindObjectsOfType<KeyDoor>();

        foreach (var o in myDoors)
        {
            o.OnDoorOpenEvent += OnDoorOpen;
            o.OnDoorCloseEvent += OnDoorClose;
            o.OnDoorOpenPermanentlyEvent += OnDoorOpenPermanently;
        }

        var myPressurePlates = GameObject.FindObjectsOfType<PressurePlate>();

        foreach (var o in myPressurePlates)
        {
            o.OnPressurePlateActivatedEvent += OnPressurePlateActivated;
            o.OnPressurePlateDeactivatedEvent += OnPressurePlateDeactivated;
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
        OnScoreUpdate(this, doorScore + keyListScore + coinListScore + pressurePlateScore + chestScore);
    }

    public void OnScoreUpdate(object sender, int totalScore)
    {
        OnScoreUpdateEvent?.Invoke(this, totalScore);
    }

    public void OnCoinsChanged(object sender, List<ColorPalette.ColorName> coinList)
    {
        coinListScore = 0;
        foreach (var o in coinList)
        {
            switch (o)
            {
                case ColorPalette.ColorName.Brown: coinListScore += 1; break;   // How many points a brown coin is worth
                case ColorPalette.ColorName.Purple: coinListScore += 3; break;  // How many points a Purple coin is worth
                default: Debug.Log("Error: Coin not assigned a color!"); break;
            }
        }
        UpdateScore();
    }
    public void OnPressurePlateActivated(object sender, ColorPalette.ColorName pressurePlateColor)
    {
        pressurePlateScore += 3;
        UpdateScore();
    }

    public void OnPressurePlateDeactivated(object sender, ColorPalette.ColorName pressurePlateColor)
    {
        pressurePlateScore -= 3;
        UpdateScore();
    }

    public void OnChestOpened(object sender, EventArgs e)
    {
        Debug.Log("a");
        chestScore += 3;
        UpdateScore();
    }
}
