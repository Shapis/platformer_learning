using System;
using UnityEngine;

public interface IDraggableEvents
{
    void OnDraggingBegins(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs);
    void OnDraggingEnds(object sender, EventArgs e);
    void OnLineOfSightBlocked(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs);
    void OnLineOfSightUnblocked(object sender, EventArgs e);

}