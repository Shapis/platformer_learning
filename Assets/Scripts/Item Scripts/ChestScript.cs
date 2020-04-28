using System;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{

    [SerializeField] private LootHandler m_LootHandler;

    private readonly List<Transform> myChestDrops = new List<Transform>(); // List of drops from this chest, all drops must be direct children of this chest object.

    private List<GameObject> myInstancedDrops = new List<GameObject>();

    private List<Vector3> myInitalPosition = new List<Vector3>();

    private bool isOpen = false;

    [SerializeField] private GameObject m_CoinsContainer;

    // Start is called before the first frame update
    void Awake()
    {
        //gameObject.GetComponent<ChestScript>().m_CoinsContainer = GameObject.Find("CoinsContainer");


        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).GetComponent<LootTable>() != null) // Make sure the item being added is something that has a loottable component before adding to list
            {
                myChestDrops.Add(gameObject.transform.GetChild(i));
                myInitalPosition.Add(gameObject.transform.GetChild(i).transform.position);
            }
        }
    }

    public void OnOpen()
    {
        if (!isOpen)
        {
            isOpen = true;
            gameObject.GetComponent<Animator>().SetBool("isOpen", true);
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x, -4.947186e-05f);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x, 0.5637438f);

            foreach (var o in myChestDrops)
            {
                myInstancedDrops.Add(m_LootHandler.DropLoot(o.gameObject.GetComponent<LootTable>().GetItemType(), transform.position, Quaternion.identity));
            }

            MoveDrops();

        }

    }

    private void MoveDrops()
    {
        for (int i = 0; i < myInstancedDrops.Count; i++)
        {

            if (myInstancedDrops[i].GetComponent<CoinScript>() != null)
            {
                myInstancedDrops[i].GetComponent<CoinScript>().Tangible = false;
            }


            switch (myInstancedDrops[i].GetComponent<LootTable>().GetItemType())
            {
                case LootTable.ItemType.BrownCoin: BrownCoin(i); break;
                case LootTable.ItemType.PurpleCoin: PurpleCoin(i); break;

            }
        }
    }

    private void PurpleCoin(int i)
    {
        LeanTween.move(myInstancedDrops[i], myInitalPosition[i], 1);
        StartCoroutine(DelayHandler.DelayAction(1, () => myInstancedDrops[i].GetComponent<CoinScript>().Tangible = true));
    }

    private void BrownCoin(int i)
    {
        StartCoroutine(DelayHandler.DelayAction(1, () => myInstancedDrops[i].GetComponent<CoinScript>().Tangible = true));
    }
}


// private void DropLoot()
// {

//     foreach (var o in myChestDrops)
//     {
//         Debug.Log("generating items");
//         Transform myItem = Instantiate(o, transform.position, Quaternion.identity, this.transform); // Instancing inside the generating chest so the original scale is preserved, object must be moved to an item container afterwards to untie the transform from the chest transform!
//         myItem.gameObject.SetActive(true);




//     }
// }




// private GameObject GenerateBrownCoin(Vector2 myVector)
// {
//     GameObject myLoot =
//         Instantiate(m_BrownCoinPrefab,
//             gameObject.transform.position,
//             Quaternion.identity);
//     myLoot.transform.SetParent(m_CoinsContainer.transform);

//     myLoot.gameObject.layer = 30; // 30 is the intangible layer

//     myLoot.transform.position =
//         gameObject.transform.position + new Vector3(0, 0.0f, 0);

//     //GameObject myBoxCollider2D = new GameObject();
//     //myBoxCollider2D.transform.parent = myLoot.transform;
//     BoxCollider2D myTriggerBoxCollider2D =
//         myLoot.GetComponent<BoxCollider2D>();

//     BoxCollider2D myBoxCollider2D = myLoot.AddComponent<BoxCollider2D>();

//     myBoxCollider2D.size = myTriggerBoxCollider2D.size;
//     myBoxCollider2D.offset = myTriggerBoxCollider2D.offset;

//     //myBoxCollider2D = myTriggerBoxCollider2D;
//     //myBoxCollider2D.isTrigger = false;
//     myLoot.AddComponent<Rigidbody2D>();

//     myLoot
//         .GetComponent<Rigidbody2D>()
//         .AddForce(new Vector2(myVector.x / 500, myVector.y / 100));
//     myLoot.GetComponent<Rigidbody2D>().freezeRotation = true;
//     myLoot.GetComponent<Rigidbody2D>().mass = 0f;
//     myLoot.GetComponent<Rigidbody2D>().collisionDetectionMode =
//         CollisionDetectionMode2D.Continuous;

//     myBrownCoinList.Add(myLoot);
//     return myLoot;
// }

// private GameObject GeneratePurpleCoin()
// {
//     GameObject myLoot =
//         Instantiate(m_PurpleCoinPrefab,
//             gameObject.transform.position,
//             Quaternion.identity);
//     myLoot.transform.SetParent(m_CoinsContainer.transform);

//     myLoot.gameObject.layer = 30; // 30 is the intangible layer

//     myLoot.transform.position =
//         gameObject.transform.position + new Vector3(0, 0.0f, 0);
//     myPurpleCoinList.Add(myLoot);

//     return myLoot;
// }
