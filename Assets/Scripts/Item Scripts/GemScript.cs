using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{

    private bool hasBeenCollided = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasBeenCollided)
        {


            Debug.Log("Gem collided with: " + collision);





            hasBeenCollided = true;

        }



    }
}
