using System;
using UnityEngine;
using static AudioClipCatalog;

public class SkullGateAudioEmitter : BaseAudioEmitter, IKeyDoorEvents
{

    private KeyDoor m_KeyDoor;
    private float _relativeVolume = 1f;
    public override void InitAwake()
    {
        m_KeyDoor = GetComponent<KeyDoor>();
    }

    public override void InitStart()
    {
        m_KeyDoor.OnDoorOpenEvent += OnDoorOpen;
        m_KeyDoor.OnDoorCloseEvent += OnDoorClose;
        // m_KeyDoor.OnDoorOpenPermanentlyEvent += OnDoorOpenPermanently;
    }

    public void OnDoorClose(object sender, EventArgs e)
    {
        PlaySfx(SfxName.PortcullisClosing, relativeVolume: _relativeVolume);
    }

    public void OnDoorOpen(object sender, EventArgs e)
    {
        PlaySfx(SfxName.PortcullisOpening, relativeVolume: _relativeVolume);
    }

    public void OnDoorOpenPermanently(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
