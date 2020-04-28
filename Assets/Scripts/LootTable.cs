using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{

    [SerializeField] private ItemType m_ItemType;


    public enum ItemType
    {
        BrownCoin,
        PurpleCoin,
    }


    public ItemType GetItemType()
    {
        return m_ItemType;
    }
}
