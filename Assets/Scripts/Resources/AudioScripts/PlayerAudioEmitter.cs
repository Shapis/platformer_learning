using System;
using UnityEngine;
using static AudioClipCatalog;

public class PlayerAudioEmitter : BaseAudioEmitter, ICharacterEvents
{
    private CharacterController2D m_CharacterController2D;


    public override void InitAwake()
    {
        m_CharacterController2D = GetComponent<CharacterController2D>();
    }

    public override void InitStart()
    {
        m_CharacterController2D.OnLandingEvent += OnLanding;
        // m_CharacterController2D.OnAirbourneEvent += OnAirbourne;
        // m_CharacterController2D.OnFallingEvent += OnFalling;
        m_CharacterController2D.OnJumpEvent += OnJump;
    }


    public void OnAirbourne(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnFalling(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnHorizontalMovementChanges(object sender, int movementDirection)
    {
        throw new NotImplementedException();
    }

    public void OnJump(object sender, EventArgs e)
    {
        PlaySfx(SfxName.JumpingNormal, relativeVolume: 0.02f);
    }

    public void OnLanding(object sender, EventArgs e)
    {
        PlaySfx(SfxName.LandingNormal, relativeVolume: 0.12f);
    }
}