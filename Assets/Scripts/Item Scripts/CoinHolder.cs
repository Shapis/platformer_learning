using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinHolder : MonoBehaviour
{
    [SerializeField] private float m_PurpleCoinIntangibleTimer;
    private List<CoinScript> coinList = new List<CoinScript>();

    private CoinScript coinForTheAlphaChange;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CoinScript>() != null)
        {
            PickUpCoin(other.gameObject.GetComponent<CoinScript>());
        }
    }

    private void Update()
    {
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
            coinForTheAlphaChange = myCoin;
            coinList.Add(myCoin);
            coinList = coinList.Distinct().ToList();
            //Debug.Log("Purple coin added!");
            myCoin.Tangible = false;
            ChangeAlpha();
            LeanTween.delayedCall(m_PurpleCoinIntangibleTimer, TurnTangible);
            gameObject.GetComponent<CharacterController2D>().DoubleJumpsRemaining++;
        }
    }

    private void TurnTangible()
    {
        coinForTheAlphaChange.Tangible = true;
        //Debug.Log("tangible!");
    }

    private void ChangeAlpha()
    {
        LeanTween.delayedCall(0 * (m_PurpleCoinIntangibleTimer / 2), LowerAlpha);
        LeanTween.delayedCall(m_PurpleCoinIntangibleTimer / 2, RaiseAlpha);
    }
    private void LowerAlpha()
    {
        LeanTween.alpha(coinForTheAlphaChange.gameObject, 0f, m_PurpleCoinIntangibleTimer / 2);
    }

    private void RaiseAlpha()
    {
        LeanTween.alpha(coinForTheAlphaChange.gameObject, 1f, m_PurpleCoinIntangibleTimer / 2);
    }
}