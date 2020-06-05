using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinGrabber : MonoBehaviour
{
    private readonly List<ColorPalette.ColorName> coinList = new List<ColorPalette.ColorName>();
    public event EventHandler<List<ColorPalette.ColorName>> OnCoinsChangedEvent;

    public void AddCoin(ColorPalette.ColorName coinType)
    {
        coinList.Add(coinType);
        OnCoinsChanged(this, coinList);

    }

    public void RemoveCoin(ColorPalette.ColorName coinType)
    {
        coinList.Remove(coinType);
        OnCoinsChanged(this, coinList);
    }

    public bool ContainsCoin(ColorPalette.ColorName coinType)
    {
        return coinList.Contains(coinType);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Coin coin = other.GetComponent<Coin>();
        if (coin != null && coin.Tangible)
        {
            Debug.Log("a");
            coin.Tangible = false;
            AddCoin(coin.GetCoinType());
            Destroy(coin.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Coin coin = other.GetComponent<Coin>();
        if (coin != null && coin.Tangible)
        {
            Debug.Log("a");
            coin.Tangible = false;
            AddCoin(coin.GetCoinType());
            Destroy(coin.gameObject);
        }
    }

    public void OnCoinsChanged(object sender, List<ColorPalette.ColorName> coinList)
    {
        OnCoinsChangedEvent?.Invoke(this, coinList);
    }
}
