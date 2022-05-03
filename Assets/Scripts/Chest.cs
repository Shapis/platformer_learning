using System.Collections.Generic;
using UnityEngine;

// Chests will move key and purple coins to their specific location once opened. But brown coins will just drop around it.

public class Chest : BaseItem
{
    public List<Transform> ChestLoot { get; private set; } = new List<Transform>();
    public List<Vector3> ChestLootInitialPositions { get; private set; } = new List<Vector3>();

    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).GetComponent<LootTable>() != null) // Make sure the item being added is something that has a LootTable component before adding to list
            {
                ChestLoot.Add(gameObject.transform.GetChild(i));
                ChestLootInitialPositions.Add(gameObject.transform.GetChild(i).transform.position);
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
