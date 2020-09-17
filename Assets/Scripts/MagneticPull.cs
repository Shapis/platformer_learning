using UnityEngine;

public class MagneticPull : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Coin>() && other.GetComponent<Magnet>() && other.GetComponent<Coin>().Tangible && other.GetComponent<Rigidbody2D>())
        {
            other.GetComponent<Rigidbody2D>().gravityScale = 0;
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(1.5f * (this.transform.position - other.transform.position).x, 1.5f * (this.transform.position - other.transform.position).y);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Coin>() && other.GetComponent<Magnet>() && other.GetComponent<Coin>().Tangible && other.GetComponent<Rigidbody2D>())
        {
            other.GetComponent<Rigidbody2D>().gravityScale = 1;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
