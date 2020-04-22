using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    private bool isOpen = false;

    private int collisionCount;


    // Start is called before the first frame update
    void Awake()
    {
        // KeyScript.KeyType keyType = gameObject.GetComponent<KeyScript>().GetKeyType();
        //gameObject.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<KeyScript>().GetKeyColor32();


    }

    private void Update()
    {
        // Debug.DrawRay(transform.position, new Vector2(0, 1));
        // Debug.DrawRay(transform.position - new Vector3(0.3f, 0, 0), new Vector2(0, 1));
        // Debug.DrawRay(transform.position + new Vector3(0.3f, 0, 0), new Vector2(0, 1));
        // Debug.Log(isOpen);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!isOpen)
        {
            if (Physics2D.Raycast(transform.position, new Vector2(0, 1))
            || Physics2D.Raycast(transform.position - new Vector3(0.3f, 0, 0), new Vector2(0, 1))
            || Physics2D.Raycast(transform.position + new Vector3(0.3f, 0, 0), new Vector2(0, 1)))
            {
                isOpen = true;
                WhenOpen();
            }
        }




    }



    private void OnCollisionExit2D(Collision2D other)
    {
        isOpen = false;
        WhenClosed();
    }
    private void WhenOpen()
    {

    }
    private void WhenClosed()
    {

    }



}
