using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBottomTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {





        PlayerCollisionHandler playerCollisionHandler = new PlayerCollisionHandler();
        playerCollisionHandler.Collided(collision, gameObject);




    }
}
