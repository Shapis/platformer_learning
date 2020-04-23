using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour
{

    [SerializeField] private KeyType keyType;

    Color[] myKeyColors;


    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().color = GetKeyColor32(keyType);
    }



    public KeyType GetKeyType()
    {
        return keyType;
    }

    public enum KeyType
    {
        Blue,
        Brown,
        Gray,
        Green,
        Purple,
        Red,
        Silver,
        Teal,
        Violet,
        White


    }
    static public Color32 GetKeyColor32(KeyScript.KeyType keyType)
    {
        Color32 myKeyColor;
        switch (keyType)
        {



            default:
            case KeyScript.KeyType.Blue: myKeyColor = new Color32(49, 58, 145, 255); break;
            case KeyScript.KeyType.Brown: myKeyColor = new Color32(146, 86, 43, 255); break;
            case KeyScript.KeyType.Gray: myKeyColor = new Color32(72, 84, 84, 255); break;
            case KeyScript.KeyType.Green: myKeyColor = new Color32(80, 148, 80, 255); break;
            case KeyScript.KeyType.Purple: myKeyColor = new Color32(118, 85, 162, 255); break;
            case KeyScript.KeyType.Red: myKeyColor = new Color32(177, 72, 99, 255); break;
            case KeyScript.KeyType.Silver: myKeyColor = new Color32(128, 128, 120, 255); break;
            case KeyScript.KeyType.Teal: myKeyColor = new Color32(143, 191, 213, 255); break;
            case KeyScript.KeyType.Violet: myKeyColor = new Color32(131, 133, 207, 255); break;
            case KeyScript.KeyType.White: myKeyColor = new Color32(237, 230, 200, 255); break;

        }


        return myKeyColor;
    }

}
