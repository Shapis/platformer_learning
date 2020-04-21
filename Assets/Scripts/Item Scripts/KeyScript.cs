using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour
{

    [SerializeField] private KeyType keyType;



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

    public KeyType GetKeyType()
    {
        return keyType;
    }

}
