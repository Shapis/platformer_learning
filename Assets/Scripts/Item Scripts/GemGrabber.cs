using UnityEngine;

public class GemGrabber : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<GemScript>() != null)
        {
            Debug.Log("collided with gem");
        }
    }

}
