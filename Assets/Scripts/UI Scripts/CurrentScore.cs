using UnityEngine;
using TMPro;
using System;

public class CurrentScore : MonoBehaviour, IKeyHolderEvents, ICoinHolderEvents, IKeyDoorEvents
{

    private int totalCoinAndKeyScore;

    private int totalActionsScore;

    private GameObject myPlayer;
    private KeyDoorScript[] myDoors;

    private void Awake()
    {

        var temp = GameObject.FindGameObjectsWithTag("Door");
        myDoors = new KeyDoorScript[temp.Length];

        for (int i = 0; i < temp.Length; i++)
        {
            myDoors[i] = temp[i].GetComponent<KeyDoorScript>();
        }

        myPlayer = GameObject.Find("Player");
    }


    private void Start()
    {

        foreach (var door in myDoors)
        {
            door.OnDoorOpenPermanentlyEvent += OnDoorOpenPermanently;
            door.OnDoorOpenEvent += OnDoorOpen;
            door.OnDoorCloseEvent += OnDoorClose;
        }

        myPlayer.GetComponent<CoinHolder>().OnCoinsChangedEvent += OnCoinsChanged;
        myPlayer.GetComponent<KeyHolderScript>().OnKeysChangedEvent += OnKeysChanged;
        UpdateVisual();

    }

    public void OnKeysChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    public void OnCoinsChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    // Update is called once per frame // this should be called once every time the number of keys or number of coins is updated, gotta fix it.
    private void UpdateVisual()
    {
        totalCoinAndKeyScore = 0;
        foreach (var o in myPlayer.GetComponent<CoinHolder>().CoinList)
        {
            switch (o.GetCoinType())
            {
                case CoinScript.CoinType.Brown: totalCoinAndKeyScore++; break;
                case CoinScript.CoinType.Purple: totalCoinAndKeyScore += 3; break;
            }
        }

        foreach (var o in myPlayer.GetComponent<KeyHolderScript>().KeyList)
        {
            totalCoinAndKeyScore += 5;
        }

        myPlayer.GetComponent<PlayerScoreKeeper>().TotalScore = totalCoinAndKeyScore + totalActionsScore;
        gameObject.GetComponent<TextMeshProUGUI>().text = (myPlayer.GetComponent<PlayerScoreKeeper>().TotalScore).ToString();

    }

    public void OnDoorOpen(object sender, EventArgs e)
    {
    }

    public void OnDoorClose(object sender, EventArgs e)
    {
    }

    public void OnDoorOpenPermanently(object sender, EventArgs e)
    {
        totalActionsScore += 8;
        UpdateVisual();
    }
}
