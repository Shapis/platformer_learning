using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyGrabber : MonoBehaviour, IKeyGrabberEvents
{
    private readonly List<ColorPalette.ColorName> keyList = new List<ColorPalette.ColorName>();
    public event EventHandler<List<ColorPalette.ColorName>> OnKeysChangedEvent;

    public void AddKey(ColorPalette.ColorName keyType)
    {
        keyList.Add(keyType);
        OnKeysChanged(this, keyList);
    }

    public void RemoveKey(ColorPalette.ColorName keyType)
    {
        keyList.Remove(keyType);
        OnKeysChanged(this, keyList);
    }

    public bool ContainsKey(ColorPalette.ColorName keyType)
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Key key = other.GetComponent<Key>();
        if (key != null && key.Tangible)
        {
            key.Tangible = false;
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }

        KeyDoor keyDoor = other.GetComponent<KeyDoor>();

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

    public void OnKeysChanged(object sender, List<ColorPalette.ColorName> keyList)
    {
        OnKeysChangedEvent?.Invoke(this, keyList);
    }
}
