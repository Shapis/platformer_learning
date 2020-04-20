using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    //[SerializeField] private BoxCollider2D myBoxCollider2D;

    // Start is called before the first frame update

    private bool isDead = false;

    //[SerializeField] LayerMask m_WhatToCollideWith;
    [SerializeField] private int m_CoinScoreValue = 1;
    [SerializeField] private int m_PurpleCoinExtraJumps = 1;

    [SerializeField] GameObject m_CoinPurple;



    void Awake()
    {




        //hysics2D.IgnoreLayerCollision(11, 11); // itemspassthrough layer cannot collide with items passthrough layer, I did this so I can spawn coins from chests properly without them colliding with each other as they spawn.

        //int playerLayer = LayerMask.NameToLayer("Player");
        //uint bitstring = (uint)m_WhatToCollideWith.value;
        //for (int i = 31; bitstring > 0; i--)
        //    if ((bitstring >> i) > 0)
        //    {
        //        bitstring = ((bitstring << 32 - i) >> 32 - i);
        //        Physics2D.IgnoreLayerCollision(gameObject.layer, i);

        //        Debug.Log(i + " ads" + playerLayer);
        //    }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (gameObject.GetComponent<BoxCollider2D>().isTrigger == true)
    //    {
    //        Destroy(gameObject.GetComponent<BoxCollider2D>());
    //        gameObject.AddComponent<BoxCollider2D>();

    //        Debug.Log("Ate a coin named: " + gameObject.name);
    //    }



    //    gameObject.GetComponent<BoxCollider2D>().isTrigger = true;



    //}





    private void OnTriggerEnter2D(Collider2D collision)
    {



        if (isDead == false && (collision.gameObject.layer == 8)) // 8 is the player layer
        {
            Destroy(gameObject);
            //Debug.Log("Coin named: " + gameObject.name + " was destroyed by: " + collision.name);
            GameObject.Find("Player").GetComponent<PlayerMovement>().currentScore += m_CoinScoreValue;
            isDead = true;
            PurpleCoin(collision.gameObject);

        }




    }


    private void PurpleCoin(GameObject myUnit)
    {
        if (gameObject == m_CoinPurple)
        {
            //Debug.Log(myUnit.GetComponentInParent<CharacterController2D>());
            myUnit.GetComponentInParent<CharacterController2D>().DoubleJumpsRemaining += m_PurpleCoinExtraJumps;
            //myUnit.GetComponent<CharacterController2D>().IsFalling = false;

        }
    }



}
