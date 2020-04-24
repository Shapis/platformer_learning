using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarmfulCollisions : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 4) // If collided with water.
        {
            Debug.Log(gameObject.name + " collided with: " + other.transform.name);
            PlayerDied myPlayerDied = new PlayerDied();
            //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2();
            //myPlayerDied.Drowned();
        }

        if (other.gameObject.layer == 12) // If collided with a harmful item.
        {
            Debug.Log(gameObject.name + " collided with: " + other.transform.name);
            PlayerDied myPlayerDied = new PlayerDied();
            myPlayerDied.Died();
        }
    }
}
