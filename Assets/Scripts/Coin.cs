using UnityEngine;

public class Coin : BaseItem
{
    [SerializeField] private ColorPalette.ColorName coinType;
    public ColorPalette.ColorName GetCoinType()
    {
        return coinType;
    }
}
