using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    // private bool isPressed = false;

    // private int collisionCount;

    // [SerializeField] Animator animator;

    // private float timer;
    // private float timeOut;

    // [SerializeField] private KeyScript.KeyType keyType;

    // private KeyDoorScript[] myDoors;


    // // Start is called before the first frame update
    // void Awake()
    // {
    //     var temp = GameObject.FindGameObjectsWithTag("Door");
    //     myDoors = new KeyDoorScript[temp.Length];

    //     for (int i = 0; i < temp.Length; i++)
    //     {
    //         myDoors[i] = temp[i].GetComponent<KeyDoorScript>();
    //     }


    //     gameObject.GetComponent<SpriteRenderer>().color = KeyScript.GetKeyColor32(keyType);


    // }

    // private void Update()
    // {

    //     Debug.DrawRay(transform.position, new Vector2(0, 1));
    //     Debug.DrawRay(transform.position - new Vector3(0.3f * transform.localScale.x, 0, 0), new Vector2(0, 1));
    //     Debug.DrawRay(transform.position + new Vector3(0.3f * transform.localScale.x, 0, 0), new Vector2(0, 1));
    // }

    // private void OnCollisionStay2D(Collision2D other)
    // {
    //     if (!isPressed)
    //     {
    //         if (Physics2D.Raycast(transform.position, new Vector2(0, 1))
    //         || Physics2D.Raycast(transform.position - new Vector3(0.3f * transform.localScale.x, 0, 0), new Vector2(0, 1))
    //         || Physics2D.Raycast(transform.position + new Vector3(0.3f * transform.localScale.x, 0, 0), new Vector2(0, 1)))
    //         {
    //             isPressed = true;
    //             WhenPressed();
    //         }
    //     }




    // }



    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     isPressed = false;
    //     WhenUnpressed();
    // }
    // private void WhenPressed()
    // {
    //     //timer = Time.time;
    //     animator.SetBool("isPressed", true);
    //     foreach (var o in myDoors)
    //     {
    //         if (o.GetKeyType() == keyType)
    //         {
    //             o.OpenDoor();
    //         }
    //     }

    //     //Debug.Log("pressed");
    // }
    // private void WhenUnpressed()
    // {
    //     animator.SetBool("isPressed", false);
    //     foreach (var o in myDoors)
    //     {
    //         if (o.GetKeyType() == keyType)
    //         {
    //             o.CloseDoor();
    //         }
    //     }
    //     //Debug.Log("unpressed");
    // }



}
