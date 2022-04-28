using System;
using System.Collections.Generic;
using UnityEngine;
using static AudioClipCatalog;

// Attach to Player prefab
// A problem in this is that the trigger for the audio doesnt happen at the top of the water graphics, but slightly below it. Which makes it seem like the audio is delayed
public class WaterGrabberAudioEmitter : BaseAudioEmitter, IWaterEvents
{
    private WaterGrabber m_WaterGrabber;
    public override void InitAwake()
    {
        m_WaterGrabber = GetComponent<WaterGrabber>();
    }

    public override void InitStart()
    {
        m_WaterGrabber.OnWaterTriggerEnter2DEvent += OnWaterTriggerEnter2D;
        // m_WaterGrabber.OnWaterTriggerExit2DEvent += OnWaterTriggerExit2D;
    }

    public void OnWaterTriggerEnter2D(object sender, EventArgs e)
    {
        PlaySfx(SfxName.WaterSplash, relativeVolume: 0.1f);
    }

    public void OnWaterTriggerExit2D(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}