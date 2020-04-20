using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    [SerializeField]
    private Transform m_Transform;

    [SerializeField]
    private float m_AnchorRight;

    [SerializeField]
    private float m_AnchorLeft;

    private Vector2 myInitialPos;

    public float CurrentDirection { get; set; }


    private bool changeDirectionX = false;

    private void Awake()

    {
        myInitialPos = m_Transform.position;
        CurrentDirection = 1f;
    }

    private void FixedUpdate()
    {
        AnchorBoundsCheck();

        //Debug.Log(myCurrentDirection);
    }

    private void AnchorBoundsCheck()
    {
        if ((m_Transform.position.x - myInitialPos.x) >= m_AnchorRight)
        {
            CurrentDirection = -1;
        }
        else if ((m_Transform.position.x - myInitialPos.x) < -m_AnchorLeft)
        {
            CurrentDirection = 1f;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log(gameObject.name + " just hit: " + collision.name);
    //}
}
