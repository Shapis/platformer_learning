using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private ColorPalette.ColorName coinType;
    public bool Tangible { get; set; } = true;

    public ColorPalette.ColorName GetCoinType()
    {
        return coinType;
    }
}
