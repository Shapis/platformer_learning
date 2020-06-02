using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private ColorPalette.ColorName keyType;
    public bool Tangible { get; set; } = true;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = ColorPalette.GetColor32(keyType);
    }

    public ColorPalette.ColorName GetKeyType()
    {
        return keyType;
    }
}
