using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    //[SerializeField] private BoxCollider2D myBoxCollider2D;

    // Start is called before the first frame update

    // private bool isDead = false;

    //[SerializeField] LayerMask m_WhatToCollideWith;
    // [SerializeField] private int m_CoinScoreValue = 1;
    // [SerializeField] private int m_PurpleCoinExtraJumps = 1;

    // [SerializeField] GameObject m_CoinPurple;

    [SerializeField] CoinType coinType;




    public enum CoinType
    {
        Brown,
        Purple,

    }





    // private void OnTriggerEnter2D(Collider2D collision)
    // {



    //     if (isDead == false && (collision.gameObject.layer == 8)) // 8 is the player layer
    //     {
    //         Destroy(gameObject);
    //         //Debug.Log("Coin named: " + gameObject.name + " was destroyed by: " + collision.name);
    //         GameObject.Find("Player").GetComponent<PlayerMovement>().currentScore += m_CoinScoreValue;
    //         isDead = true;
    //         PurpleCoin(collision.gameObject);

    //     }




    // }


    // private void PurpleCoin(GameObject myUnit)
    // {
    //     if (gameObject == m_CoinPurple)
    //     {
    //         //Debug.Log(myUnit.GetComponentInParent<CharacterController2D>());
    //         myUnit.GetComponentInParent<CharacterController2D>().DoubleJumpsRemaining += m_PurpleCoinExtraJumps;
    //         //myUnit.GetComponent<CharacterController2D>().IsFalling = false;

    //     }
    // }

    public CoinType GetCoinType()
    {
        return coinType;
    }



}
