using System;
using UnityEngine;

public class PlayerMagic : MonoBehaviour, IDraggableEvents
{
    [SerializeField] private PlayerItemDragger m_PlayerItemDragger;
    [SerializeField] private LightningScript m_LightningScript;

    private void Start()
    {
        m_PlayerItemDragger.OnDraggingBeginsEvent += OnDraggingBegins;
        m_PlayerItemDragger.OnDraggingEndsEvent += OnDraggingEnds;
        m_PlayerItemDragger.OnLineOfSightBlockedEvent += OnLineOfSightBlocked;
        m_PlayerItemDragger.OnLineOfSightUnblockedEvent += OnLineOfSightUnblocked;
    }

    public void OnDraggingBegins(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs)
    {
        m_LightningScript.Begin(draggingEventArgs.OriginGameObject.transform, draggingEventArgs.TargetGameObject.transform, draggingEventArgs.ClosestBlockingGameObject);
    }

    public void OnDraggingEnds(object sender, EventArgs e)
    {
        m_LightningScript.End();
    }

    public void OnLineOfSightBlocked(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs)
    {
        m_LightningScript.ClosestBlockingObject = draggingEventArgs.ClosestBlockingGameObject;
    }

    public void OnLineOfSightUnblocked(object sender, EventArgs e)
    {
        m_LightningScript.ClosestBlockingObject = null;
    }
}
