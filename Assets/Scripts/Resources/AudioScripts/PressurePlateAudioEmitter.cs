using System;
using System.Collections.Generic;
using UnityEngine;
using static AudioClipCatalog;

// Attach to Pressure Plate prefab
public class PressurePlateAudioEmitter : BaseAudioEmitter, IPressurePlateEvents
{
    private PressurePlate m_PressurePlate;
    public override void InitAwake()
    {
        m_PressurePlate = GetComponent<PressurePlate>();
    }

    public override void InitStart()
    {
        m_PressurePlate.OnPressurePlateActivatedEvent += OnPressurePlateActivated;
        m_PressurePlate.OnPressurePlateDeactivatedEvent += OnPressurePlateDeactivated;
    }

    public void OnPressurePlateActivated(object sender, ColorPalette.ColorName pressurePlateColor)
    {
        PlaySfx(SfxName.ContinuousPressurePlateBuzz, loop: true, relativeVolume: 0.05f);
    }

    public void OnPressurePlateDeactivated(object sender, ColorPalette.ColorName pressurePlateColor)
    {
        StopSfx();
    }
}