using System.Collections;
using UnityEngine;

public class PlayerHarmfulCollisions : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BuoyancyEffector2D>() != null) // If collided with water.
        {
            PlayerDied myPlayerDied = new PlayerDied();
            myPlayerDied.Drowned();
            //SlowPlayerDown();
            //Debug.Log("trigger enter");
        }
    }

    private void SlowPlayerDown()
    {
        //Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity.y);

        if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= -6f)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -6f);
        }
    }
}
