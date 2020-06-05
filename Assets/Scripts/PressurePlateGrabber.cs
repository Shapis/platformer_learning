using UnityEngine;

public class PressurePlateGrabber : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        PressurePlate pressurePlate = other.gameObject.GetComponent<PressurePlate>();
        if (pressurePlate != null && pressurePlate.Tangible)
        {
            pressurePlate.Activate();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        PressurePlate pressurePlate = other.gameObject.GetComponent<PressurePlate>();
        if (pressurePlate != null && pressurePlate.Tangible)
        {
            pressurePlate.Deactivate();
        }
    }
}
