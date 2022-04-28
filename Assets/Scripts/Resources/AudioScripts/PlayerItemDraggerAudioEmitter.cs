using System;
using UnityEngine;
using static AudioClipCatalog;

public class PlayerItemDraggerAudioEmitter : BaseAudioEmitter, IDraggableEvents
{
    private PlayerItemDragger m_PlayerItemDragger;

    public override void InitAwake()
    {
        m_PlayerItemDragger = GetComponent<PlayerItemDragger>();
    }

    public override void InitStart()
    {
        m_PlayerItemDragger.OnDraggingBeginsEvent += OnDraggingBegins;
        m_PlayerItemDragger.OnDraggingEndsEvent += OnDraggingEnds;
    }

    public void OnDraggingBegins(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs)
    {
        PlaySfx(SfxName.ConstantBuzz, loop: true, relativeVolume: 0.5f, pitch: 1f);
    }

    public void OnDraggingEnds(object sender, EventArgs e)
    {
        StopSfx();
    }

    public void OnLineOfSightBlocked(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs)
    {
        throw new NotImplementedException();
    }

    public void OnLineOfSightUnblocked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}