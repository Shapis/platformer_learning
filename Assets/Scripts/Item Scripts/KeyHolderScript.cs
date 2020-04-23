using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolderScript : MonoBehaviour, IKeyHolderEvents
{


    public event EventHandler OnKeysChangedEvent;
    private List<KeyScript.KeyType> keyList;

    private float timer = 0f;
    private float timeOut = 0.1f;

    private bool keyPickup = true;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeOut)
        {
            timer = 0;
            keyPickup = true;

        }
    }

    private void Awake()
    {
        keyList = new List<KeyScript.KeyType>();
    }

    public void AddKey(KeyScript.KeyType keyType)
    {
        if (keyPickup)
        {
            keyPickup = false;
            //Debug.Log("Added key: " + keyType);
            keyList.Add(keyType);
            OnKeysChanged();
        }
    }



    public void RemoveKey(KeyScript.KeyType keyType)
    {
        if (keyPickup)
        {
            keyPickup = false;
            // Debug.Log("Removed key");
            keyList.Remove(keyType);
            OnKeysChanged();
        }
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
                keyDoor.OpenDoorPermanently();

            }
        }
    }

    public List<KeyScript.KeyType> GetKeyList()
    {
        return keyList;
    }

    public void OnKeysChanged()
    {
        OnKeysChangedEvent?.Invoke(this, EventArgs.Empty);
    }


}
