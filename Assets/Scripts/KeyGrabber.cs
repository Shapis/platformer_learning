using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyGrabber : MonoBehaviour, IKeyGrabberEvents
{
    private readonly List<ColorPalette.ColorName> keyList = new List<ColorPalette.ColorName>();
    public event EventHandler<List<ColorPalette.ColorName>> OnKeysChangedEvent;
    public event EventHandler<ColorPalette.ColorName> OnKeyAddedEvent;
    public event EventHandler<ColorPalette.ColorName> OnKeyRemovedEvent;

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
            OnKeyAdded(this, key.GetKeyType());
            Destroy(key.gameObject);
        }

        KeyDoor keyDoor = other.GetComponent<KeyDoor>();

        if (keyDoor != null)
        {
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                // Currently holding Key to open this door;
                OnKeyRemoved(this, keyDoor.GetKeyType());
                keyDoor.OpenDoorPermanently();
            }
        }
    }

    public void OnKeysChanged(object sender, List<ColorPalette.ColorName> keyList)
    {
        OnKeysChangedEvent?.Invoke(this, keyList);
    }

    public void OnKeyAdded(object sender, ColorPalette.ColorName keyType)
    {
        keyList.Add(keyType);
        OnKeyAddedEvent?.Invoke(this, keyType);
        OnKeysChanged(this, keyList);
    }

    public void OnKeyRemoved(object sender, ColorPalette.ColorName keyType)
    {
        keyList.Remove(keyType);
        OnKeyRemovedEvent?.Invoke(this, keyType);
        OnKeysChanged(this, keyList);
    }
}
