using UnityEngine;

public class LootHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_BrownCoin;
    [SerializeField] private GameObject m_PurpleCoin;
    [SerializeField] private GameObject m_Key;

    private Transform temp;

    public GameObject DropLoot(LootTable.ItemType i, Vector3 position, Quaternion quaternion)
    {
        switch (i)
        {
            case LootTable.ItemType.BrownCoin: temp = m_BrownCoin.transform; break;
            case LootTable.ItemType.PurpleCoin: temp = m_PurpleCoin.transform; break;
            case LootTable.ItemType.Key: temp = m_Key.transform; break;
        }

        return InstantiateLoot(temp.gameObject, position, quaternion);
    }

    private GameObject InstantiateLoot(GameObject myLoot, Vector3 position, Quaternion quaternion)
    {
        return Instantiate(myLoot, position, quaternion);
    }
}
