using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private CoinType coinType;

    public bool Tangible { get; set; } = true;

    public enum CoinType
    {
        Brown,
        Purple,

    }

    public CoinType GetCoinType()
    {
        return coinType;
    }
}