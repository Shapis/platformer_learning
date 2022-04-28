using System;
using UnityEngine;
using static AudioClipCatalog;

public class ChestAudioEmitter : BaseAudioEmitter, IChestGrabberEvents
{
    private ChestGrabber m_ChestGrabber;
    public override void InitAwake()
    {
        m_ChestGrabber = FindObjectOfType<ChestGrabber>(); // There should onl ever be 1 chest grabber in the scene, which is attached to the Player
    }

    public override void InitStart()
    {
        m_ChestGrabber.OnChestOpenedEvent += OnChestOpened;
    }

    public void OnChestOpened(object sender, EventArgs e)
    {
        PlaySfx(SfxName.ChestOpening, relativeVolume: 0.5f);
    }

    // Gotta double check there's no bugs in the sound normalization here
    private void OnCollisionEnter2D(Collision2D other)
    {
        float vol = 0.15f;
        if (other.gameObject.layer == 12 || other.gameObject.layer == 10) // Checks if the collision happened in the tiles layer or the items layer
        {
            if (other.relativeVelocity.magnitude > 2 && other.relativeVelocity.magnitude < 10f)
            {
                PlaySfx(SfxName.MetalFalling, relativeVolume: vol * (other.relativeVelocity.magnitude - 2f) / (10f - 2f));
            }
            else if (other.relativeVelocity.magnitude >= 10f)
            {
                PlaySfx(SfxName.MetalFalling, relativeVolume: vol);
            }
        }

    }
}

