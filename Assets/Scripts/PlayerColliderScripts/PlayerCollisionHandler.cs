using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler
{
    

public void Collided(Collider2D collision, GameObject myGameObject)
    {
        if (collision.gameObject.layer == 9 && collision.gameObject.GetComponent<Animator>().GetBool("isDead") == false) // 9 is the layer for enemies. 
        {
            //m_PlayerMovement.Jump();
            //Destroy(collision.gameObject);
            Debug.Log(myGameObject.name + " collided with: " + collision.name);


            PlayerDied myPlayerDied = new PlayerDied();
            myPlayerDied.Died();

        }

        if (collision.gameObject.layer == 4) // if collided with water.
        {
            Debug.Log(myGameObject.name + " collided with: " + collision.name);
            PlayerDied myPlayerDied = new PlayerDied();
            myPlayerDied.Drowned();
        }

        if (collision.gameObject.layer == 12) // if collided with harmful items;
        {

            Debug.Log(myGameObject.name + " collided with: " + collision.name);
            PlayerDied myPlayerDied = new PlayerDied();
            myPlayerDied.Died();
        }



    }










}
