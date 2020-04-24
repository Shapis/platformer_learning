using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private readonly CoinType coinType;

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