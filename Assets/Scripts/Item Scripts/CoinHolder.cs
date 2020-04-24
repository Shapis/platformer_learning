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
            PickUpCoin(other.gameObject);
        }
    }

    private void PickUpCoin(GameObject myCoin)
    {
        switch (myCoin.GetComponent<CoinScript>().GetCoinType())
        {
            case CoinScript.CoinType.Brown: BrownCoin(myCoin); break;
            case CoinScript.CoinType.Purple: PurpleCoin(); break;
        }
    }

    private void BrownCoin(GameObject o)
    {
        Destroy(o);
    }

    private void PurpleCoin()
    {
        Debug.Log("collided with purple coin");
    }
}