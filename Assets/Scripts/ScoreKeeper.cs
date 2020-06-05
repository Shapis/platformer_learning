using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour, IKeyGrabberEvents, IKeyDoorEvents
{
    [SerializeField] private GameObject m_Player;

    public void OnDoorClose(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnDoorOpen(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnDoorOpenPermanently(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnKeysChanged(object sender, List<ColorPalette.ColorName> keyList)
    {
        throw new System.NotImplementedException();
    }
}
