using System;
using UnityEngine;
using static AudioClipCatalog;

public class BoxAudioEmitter : BaseAudioEmitter
{
    public override void InitAwake()
    {

    }

    public override void InitStart()
    {

    }

    // Gotta double check there's no bugs in the sound normalization here
    private void OnCollisionEnter2D(Collision2D other)
    {
        float vol = 0.3f;
        if (other.gameObject.layer == 12 || other.gameObject.layer == 10) // Checks if the collision happened in the tiles layer or the items layer
        {
            if (other.relativeVelocity.magnitude > 2 && other.relativeVelocity.magnitude < 10f)
            {
                PlaySfx(SfxName.HollowWoodKnock, relativeVolume: vol * (other.relativeVelocity.magnitude - 2f) / (10f - 2f));
            }
            else if (other.relativeVelocity.magnitude >= 10f)
            {
                PlaySfx(SfxName.HollowWoodKnock, relativeVolume: vol);
            }
        }

    }
}

