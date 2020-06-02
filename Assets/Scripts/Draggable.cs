using System;
using UnityEngine;

public class Draggable : MonoBehaviour, IDraggableEvents
{
    public void OnDraggingBegins(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs)
    {
        throw new NotImplementedException();
    }

    public void OnDraggingEnds(object sender, EventArgs e)
    {
        throw new NotImplementedException();
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
