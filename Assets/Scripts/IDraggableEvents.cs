using System;
using UnityEngine;

public interface IDraggableEvents
{
    void OnDraggingBegins(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs); // Invoked from PlayerItemDragger.cs
    void OnDraggingEnds(object sender, EventArgs e); // Invoked from PlayerItemDragger.cs
    void OnLineOfSightBlocked(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs); // Invoked from PlayerItemDragger.cs
    void OnLineOfSightUnblocked(object sender, EventArgs e); // Invoked from PlayerItemDragger.cs
}