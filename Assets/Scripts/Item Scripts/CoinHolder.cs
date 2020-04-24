using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinHolder : MonoBehaviour
{

    private List<CoinScript.CoinType> coinList;
    private void OnTriggerEnter2D(Collider2D other)
    {
        CoinScript coin = other.GetComponent<CoinScript>();
        if (coin != null)
        {
            PickUpCoin(coin.GetCoinType());
        }


    }

    private void PickUpCoin(CoinScript.CoinType coinType)
    {

        switch (coinType)
        {
            default:
            case CoinScript.CoinType.Brown: BrownCoin(); break;
            case CoinScript.CoinType.Purple: PurpleCoin(); break;
        }

    }

    private void BrownCoin()
    {

    }

    private void PurpleCoin()
    {

    }
}
