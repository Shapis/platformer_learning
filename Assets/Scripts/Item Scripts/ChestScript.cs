using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour {
    private bool isOpen = false;

    [SerializeField] private readonly GameObject m_BrownCoinPrefab;

    [SerializeField] private readonly GameObject m_PurpleCoinPrefab;

    [SerializeField]
    private GameObject m_CoinsContainer;

    [SerializeField] private readonly float m_BrownCoinIntangibilityTime;

    [SerializeField] private readonly float m_PurpleCoinIntangibilityTime = 3f;

    [SerializeField] private readonly int m_BrownCoinDrops = 3;

    [SerializeField]
    private Transform[] m_PurpleCoinDrops;

    private readonly List<GameObject> myBrownCoinList = new List<GameObject> ();

    private readonly List<GameObject> myPurpleCoinList = new List<GameObject> ();

    // VARIABLES FOR THE ACCELERATION/DECELERATION OF THE PURPLE COINS.
    private float myCurrentTime;

    private Vector2[] myMaxSpeed;

    private Vector2[] myAcceleration;

    // Start is called before the first frame update
    void Awake () {
        m_PurpleCoinDrops = new Transform[gameObject.transform.childCount];

        //Physics2D.IgnoreLayerCollision(8, 30, true); // Ignore Collision between player and layer 30
        //Physics2D.IgnoreLayerCollision(30, 30, true); // ignore collision between layer 30 and layer 30
        gameObject.GetComponent<ChestScript> ().m_CoinsContainer =
            GameObject.Find ("CoinsContainer");

        //DropLoot(new Vector2(2,2000000));
        myAcceleration = new Vector2[m_PurpleCoinDrops.Length];

        myMaxSpeed = new Vector2[m_PurpleCoinDrops.Length];

        for (int i = 0; i < m_PurpleCoinDrops.Length; i++) {
            m_PurpleCoinDrops[i] = gameObject.transform.GetChild (i).transform; // populate the transforms of the PURPLE coin drops from the inactive PURPLE coins that children of the chest game object.
        }
    }

    private void Start () { }

    // Update is called once per frame
    void FixedUpdate () {
        if (Time.time >= myCurrentTime + m_BrownCoinIntangibilityTime) {
            //Physics2D.IgnoreLayerCollision(8, 30, false);
            foreach (GameObject o in myBrownCoinList) {
                if (o != null) {
                    Destroy (o.GetComponent<Rigidbody2D> ());
                    o.layer = 11; // move the coins in the array to the passthrough layer.
                }
            }
        }

        if (Time.time >= myCurrentTime + m_PurpleCoinIntangibilityTime) {
            foreach (GameObject o in myPurpleCoinList) {
                if (o != null) {
                    o.layer = 11; // move the coins in the array to the passthrough layer.
                }
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (
            (collision.gameObject.layer == 8) && !isOpen // 8 is the player layer
        ) {
            isOpen = true;
            gameObject.GetComponent<Animator> ().SetBool ("isOpen", true);

            DropLoot (m_BrownCoinDrops, m_PurpleCoinDrops.Length);

            for (int i = 0; i < myPurpleCoinList.Count; i++) {
                LeanTween
                    .move (myPurpleCoinList[i],
                        m_PurpleCoinDrops[i].position,
                        m_PurpleCoinIntangibilityTime);
            }

            myCurrentTime = Time.time; // Start a timer that's going to be used to decide when the collision between coins and players should stop happening.

            gameObject.GetComponent<BoxCollider2D> ().offset =
                new Vector2 (gameObject.GetComponent<BoxCollider2D> ().offset.x, -4.947186e-05f);
            gameObject.GetComponent<BoxCollider2D> ().size =
                new Vector2 (gameObject.GetComponent<BoxCollider2D> ().size.x,
                    0.5637438f);
        }
    }

    private void DropLoot (int numberOfBrownCoins, int NumberOfPurpleCoins) {
        for (int i = 0; i < numberOfBrownCoins; i++) {
            float isEven = -1f;

            if ((i % 2) == 0) {
                isEven = 1f;
            }

            GenerateBrownCoin (new Vector2 (isEven * i, 2f));
        }

        for (int i = 0; i < NumberOfPurpleCoins; i++) {
            GeneratePurpleCoin ();
        }
    }

    private GameObject GenerateBrownCoin (Vector2 myVector) {
        GameObject myLoot =
            Instantiate (m_BrownCoinPrefab,
                gameObject.transform.position,
                Quaternion.identity);
        myLoot.transform.SetParent (m_CoinsContainer.transform);

        myLoot.gameObject.layer = 30; // 30 is the intangible layer

        myLoot.transform.position =
            gameObject.transform.position + new Vector3 (0, 0.0f, 0);

        //GameObject myBoxCollider2D = new GameObject();
        //myBoxCollider2D.transform.parent = myLoot.transform;
        BoxCollider2D myTriggerBoxCollider2D =
            myLoot.GetComponent<BoxCollider2D> ();

        BoxCollider2D myBoxCollider2D = myLoot.AddComponent<BoxCollider2D> ();

        myBoxCollider2D.size = myTriggerBoxCollider2D.size;
        myBoxCollider2D.offset = myTriggerBoxCollider2D.offset;

        //myBoxCollider2D = myTriggerBoxCollider2D;
        //myBoxCollider2D.isTrigger = false;
        myLoot.AddComponent<Rigidbody2D> ();

        myLoot
            .GetComponent<Rigidbody2D> ()
            .AddForce (new Vector2 (myVector.x / 500, myVector.y / 100));
        myLoot.GetComponent<Rigidbody2D> ().freezeRotation = true;
        myLoot.GetComponent<Rigidbody2D> ().mass = 0f;
        myLoot.GetComponent<Rigidbody2D> ().collisionDetectionMode =
            CollisionDetectionMode2D.Continuous;

        myBrownCoinList.Add (myLoot);
        return myLoot;
    }

    private GameObject GeneratePurpleCoin () {
        GameObject myLoot =
            Instantiate (m_PurpleCoinPrefab,
                gameObject.transform.position,
                Quaternion.identity);
        myLoot.transform.SetParent (m_CoinsContainer.transform);

        myLoot.gameObject.layer = 30; // 30 is the intangible layer

        myLoot.transform.position =
            gameObject.transform.position + new Vector3 (0, 0.0f, 0);
        myPurpleCoinList.Add (myLoot);

        return myLoot;
    }
}