using UnityEngine;

public class ChestGrabber : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<ChestScript>() != null)
        {
            // Debug.Log(other.gameObject);
            other.gameObject.GetComponent<ChestScript>().OnOpen();
        }
    }
}
