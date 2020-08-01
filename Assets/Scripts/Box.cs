using UnityEngine;

public class Box : MonoBehaviour
{
    private void Start()
    {
        // If the color of the box is white (meaning it hasn't been assigned yet) then pick a random color for the box from the palette (except black) and color it.
        if (Color.white == gameObject.GetComponent<SpriteRenderer>().color)
        {
            int rInt = Random.Range(1, 16); // Range between 1 and 15. Black is color 0 and black boxes look weird.
            gameObject.GetComponent<SpriteRenderer>().color = ColorPalette.GetColor32((ColorPalette.ColorName)rInt);
        }
    }
}

