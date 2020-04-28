using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinHolder : MonoBehaviour, ICoinHolderEvents
{
    [SerializeField] private float m_PurpleCoinIntangibleTimer;
    // private List<CoinScript> CoinList = new List<CoinScript>();

    public List<CoinScript> CoinList { get; set; } = new List<CoinScript>();

    public event EventHandler OnCoinsChangedEvent;

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
        OnCoinsChanged();
    }

    private void BrownCoin(CoinScript myCoin)
    {
        if (myCoin.Tangible)
        {
            CoinList.Add(myCoin);
            myCoin.Tangible = false;
            myCoin.gameObject.SetActive(false);
            //gameObject.GetComponent<PlayerScoreKeeper>().CurrentScore++;

        }
    }

    private void PurpleCoin(CoinScript myCoin)
    {
        if (myCoin.Tangible)
        {

            CoinList.Add(myCoin);
            CoinList = CoinList.Distinct().ToList();

            myCoin.Tangible = false;

            LeanTween.alpha(myCoin.gameObject, 0f, m_PurpleCoinIntangibleTimer / 2);

            StartCoroutine(DelayHandler.DelayAction(m_PurpleCoinIntangibleTimer / 2, () => LeanTween.alpha(myCoin.gameObject, 1f, m_PurpleCoinIntangibleTimer / 2)));

            StartCoroutine(DelayHandler.DelayAction(m_PurpleCoinIntangibleTimer, () => myCoin.Tangible = true)); // Sets the CoinScript back to Tangible = true after a delay.

            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 14f);

        }
    }

    private void OnCoinsChanged()
    {
        OnCoinsChangedEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnCoinsChanged(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}