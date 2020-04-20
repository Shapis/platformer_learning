using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnderneathTrigger : MonoBehaviour
{
    [SerializeField]
    PlayerMovement m_PlayerMovement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            collision.gameObject.layer == 9 &&
            collision.gameObject.GetComponent<Animator>().GetBool("isDead") ==
            false // 9 is the layer for enemies.
        )
        {
            m_PlayerMovement.CollisionJump();
            collision
                .gameObject
                .GetComponent<Animator>()
                .SetBool("isDead", true);

            //Destroy(collision.gameObject);
            //Debug.Log(gameObject.name + " collided with: " + collision.name);
            collision.gameObject.GetComponent<BasicEnemyAI>().CurrentDirection =
                0f;
            collision.gameObject.GetComponent<BasicEnemyAI>().enabled = false;

            //collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject
                .Find("Player")
                .GetComponent<PlayerMovement>()
                .currentScore +=
                collision.GetComponent<EnemyMovement>().m_ScoreValue;
            //Time.timeScale = 0.1f;
        }
    }
}
