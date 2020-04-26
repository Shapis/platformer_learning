using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinHolder : MonoBehaviour
{
    [SerializeField] private float m_PurpleCoinIntangibleTimer;
    private List<CoinScript> coinList = new List<CoinScript>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CoinScript>() != null)
        {
            PickUpCoin(other.gameObject.GetComponent<CoinScript>());
        }
    }

    private void PickUpCoin(CoinScript myCoin)
    {
        switch (myCoin.GetComponent<CoinScript>().GetCoinType())
        {
            case CoinScript.CoinType.Brown: BrownCoin(myCoin); break;
            case CoinScript.CoinType.Purple: PurpleCoin(myCoin); break;
        }
    }

    private void BrownCoin(CoinScript myCoin)
    {
        if (myCoin.Tangible)
        {
            coinList.Add(myCoin);
            //Debug.Log("Brown coin added!");
            myCoin.Tangible = false;
            myCoin.gameObject.SetActive(false);
            // Debug.Log(coinList.Count);
        }
    }

    private void PurpleCoin(CoinScript myCoin)
    {
        if (myCoin.Tangible)
        {
            coinList.Add(myCoin);
            coinList = coinList.Distinct().ToList();
            //Debug.Log("Purple coin added!");

            myCoin.Tangible = false;

            LeanTween.alpha(myCoin.gameObject, 0f, m_PurpleCoinIntangibleTimer / 2);

            StartCoroutine(DelayHandler.DelayAction(m_PurpleCoinIntangibleTimer / 2, () => LeanTween.alpha(myCoin.gameObject, 1f, m_PurpleCoinIntangibleTimer / 2)));

            StartCoroutine(DelayHandler.DelayAction(m_PurpleCoinIntangibleTimer, () => myCoin.Tangible = true)); // Sets the CoinScript back to Tangible = true after a delay.

            //Debug.Log("Intangible: " + myCoin.transform.name);
            //gameObject.GetComponent<CharacterController2D>().DoubleJumpsRemaining++;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 14f);
        }
    }
}