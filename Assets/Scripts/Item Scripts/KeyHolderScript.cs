using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolderScript : MonoBehaviour
{

    private List<KeyScript.KeyType> keyList;

    private void Awake()
    {
        keyList = new List<KeyScript.KeyType>();
    }

    public void AddKey(KeyScript.KeyType keyType)
    {
        Debug.Log("Added key: " + keyType);
        keyList.Add(keyType);
    }

    public void RemoveKey(KeyScript.KeyType keyType)
    {
        Debug.Log("Removed key");
        keyList.Remove(keyType);
    }

    public bool ContainsKey(KeyScript.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        KeyScript key = other.GetComponent<KeyScript>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }

        KeyDoorScript keyDoor = other.GetComponent<KeyDoorScript>();

        if (keyDoor != null)
        {
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                // Currently holding Key to open this door;
                RemoveKey(keyDoor.GetKeyType());
                keyDoor.OpenDoor();

            }
        }
    }

}
