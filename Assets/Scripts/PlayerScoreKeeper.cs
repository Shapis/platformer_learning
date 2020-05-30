using System;
using UnityEngine;

public class PlayerScoreKeeper : MonoBehaviour
{
    public int TotalScore { get; set; } = 0;

    public int TotalActionsScore { get; set; } = 0;

    public int TotalCoinAndKeysScore { get; set; } = 0;

    public float FinalTotalTime { get; set; }

    public float CurrentTotalTime { get; set; }

    private KeyDoorScript[] myDoors;

    private void Awake()
    {
        GetAllDoors();

        foreach (var door in myDoors)
        {
            door.OnDoorOpenPermanentlyEvent += OnDoorOpenPermanently;
        }

        gameObject.GetComponent<CoinHolder>().OnCoinsChangedEvent += UpdateScore;
        gameObject.GetComponent<KeyHolderScript>().OnKeysChangedEvent += UpdateScore;
        gameObject.GetComponent<GemGrabber>().OnLevelEndsEvent += (a, b) => { FinalTotalTime = CurrentTotalTime; };
    }

    private void Update()
    {
        CurrentTotalTime = Time.timeSinceLevelLoad;
    }

    private void UpdateScore(object sender, EventArgs e)
    {
        TotalCoinAndKeysScore = 0;
        foreach (var o in gameObject.GetComponent<CoinHolder>().CoinList)
        {
            switch (o.GetCoinType())
            {
                case CoinScript.CoinType.Brown: TotalCoinAndKeysScore++; break;
                case CoinScript.CoinType.Purple: TotalCoinAndKeysScore += 3; break;
            }
        }

        foreach (var o in gameObject.GetComponent<KeyHolderScript>().KeyList)
        {
            TotalCoinAndKeysScore += 5;
        }

        TotalScore = TotalCoinAndKeysScore + TotalActionsScore;
    }

    private void GetAllDoors()
    {
        var temp = GameObject.FindGameObjectsWithTag("Door");
        myDoors = new KeyDoorScript[temp.Length];

        for (int i = 0; i < temp.Length; i++)
        {
            myDoors[i] = temp[i].GetComponent<KeyDoorScript>();
        }
    }

    private void OnDoorOpenPermanently(object sender, EventArgs e)
    {
        TotalActionsScore += 8;
        UpdateScore(this, EventArgs.Empty);
    }
}
