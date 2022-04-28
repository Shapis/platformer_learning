using System;
using System.Collections.Generic;
using UnityEngine;
using static AudioClipCatalog;

public class CoinAudioEmitter : BaseAudioEmitter, ICoinEvents
{

    private CoinGrabber m_CoinGrabber;
    public override void InitAwake()
    {
        m_CoinGrabber = GetComponent<CoinGrabber>();
    }

    public override void InitStart()
    {
        m_CoinGrabber.OnCoinsChangedEvent += OnCoinsChanged;
    }

    public void OnCoinsChanged(object sender, List<ColorPalette.ColorName> coinList)
    {
        PlaySfx(SfxName.CoinCollecting, relativeVolume: 0.05f);
    }
}