using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildColliderScript : MonoBehaviour
{

    [SerializeField] Component myComponent;
    private void OnCollisionEnter2D(Collision2D other)
    {
        //transform.parent.GetComponent<myComponent>().CollisionDetected(this);
    }
}
