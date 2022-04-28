using System;
using System.Collections.Generic;
using UnityEngine;
using static AudioClipCatalog;

// Attach to Player prefab
public class KeyGrabberAudioEmitter : BaseAudioEmitter, IKeyGrabberEvents
{
    private KeyGrabber m_KeyGrabber;
    public override void InitAwake()
    {
        m_KeyGrabber = GetComponent<KeyGrabber>();
    }

    public override void InitStart()
    {
        m_KeyGrabber.OnKeyAddedEvent += OnKeyAdded;
        m_KeyGrabber.OnKeyRemovedEvent += OnKeyRemoved;
    }

    public void OnKeyAdded(object sender, ColorPalette.ColorName keyType)
    {
        PlaySfx(SfxName.KeyObtained, relativeVolume: 0.12f, pitch: 1.5f);
    }

    public void OnKeyRemoved(object sender, ColorPalette.ColorName keyType)
    {
        PlaySfx(SfxName.KeyTurning, relativeVolume: 0.3f, pitch: 1f);
    }

    public void OnKeysChanged(object sender, List<ColorPalette.ColorName> keyList)
    {
        throw new NotImplementedException();
    }
}