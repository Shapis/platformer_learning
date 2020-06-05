using System;
using System.Collections.Generic;
using UnityEngine;

public class ChestGrabber : MonoBehaviour, IChestGrabberEvents
{
    [Header("Dependencies")]
    private LootHandler m_LootHandler;
    private GameObject m_CoinsContainer;
    public event EventHandler OnChestOpenedEvent;
    private List<GameObject> instancedDrops = new List<GameObject>();
    private int brownCoinCount = 0;

    void Start()
    {
        m_CoinsContainer = GameObject.Find("CoinsContainer");
        m_LootHandler = FindObjectOfType<LootHandler>();
    }

    public void OpenChest(Chest myChest)
    {
        myChest.gameObject.GetComponent<Animator>().SetBool("isOpen", true);
        myChest.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(myChest.gameObject.GetComponent<BoxCollider2D>().offset.x, -4.947186e-05f);
        myChest.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(myChest.gameObject.GetComponent<BoxCollider2D>().size.x, 0.5637438f);
        foreach (var o in myChest.ChestLoot)
        {
            instancedDrops.Add(m_LootHandler.DropLoot(o.gameObject.GetComponent<LootTable>().GetItemType(), myChest.transform.position, Quaternion.identity));
        }
        MoveDrops(myChest.ChestLootInitialPositions);
    }

    private void MoveDrops(List<Vector3> myInitialPositions)
    {
        for (int i = 0; i < instancedDrops.Count; i++)
        {
            if (instancedDrops[i].GetComponent<Coin>() != null)
            {
                instancedDrops[i].GetComponent<Coin>().Tangible = false;
            }
            switch (instancedDrops[i].GetComponent<LootTable>().GetItemType())
            {
                case LootTable.ItemType.BrownCoin: BrownCoin(i); break;
                case LootTable.ItemType.PurpleCoin: PurpleCoin(i); break;
            }
        }

        void PurpleCoin(int i)
        {
            LeanTween.move(instancedDrops[i], myInitialPositions[i], 1);
            instancedDrops[i].transform.SetParent(m_CoinsContainer.transform);
            StartCoroutine(DelayHandler.DelayAction(1, () => instancedDrops[i].GetComponent<Coin>().Tangible = true));
        }

        void BrownCoin(int i)
        {
            Rigidbody2D rb = instancedDrops[i].AddComponent<Rigidbody2D>();
            BoxCollider2D boxTriggerCollider2D = instancedDrops[i].GetComponent<BoxCollider2D>();

            GameObject container = new GameObject();
            container.transform.parent = instancedDrops[i].transform;
            container.layer = 30; // The layer that only interacts with tiles/barriers/water
            BoxCollider2D myTileCollider = container.AddComponent<BoxCollider2D>();

            myTileCollider.transform.position = instancedDrops[i].transform.position;
            myTileCollider.size = instancedDrops[i].GetComponent<BoxCollider2D>().size;

            rb.useAutoMass = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            instancedDrops[i].transform.parent = m_CoinsContainer.transform;
            if (i % 2 == 0)
            {
                rb.AddForce(new Vector2((brownCoinCount) * 4, 60));
            }
            else
            {
                rb.AddForce(new Vector2(-(brownCoinCount) * 4, 60));
            }
            brownCoinCount++;
            StartCoroutine(DelayHandler.DelayAction(1, () => instancedDrops[i].GetComponent<Coin>().Tangible = true));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Chest>() != null && other.gameObject.GetComponent<Chest>().Tangible)
        {
            other.gameObject.GetComponent<Chest>().Tangible = false;
            OpenChest(other.gameObject.GetComponent<Chest>());
        }
    }

    public void OnChestOpened(object sender, EventArgs e)
    {
        OnChestOpenedEvent?.Invoke(this, EventArgs.Empty);
    }
}
