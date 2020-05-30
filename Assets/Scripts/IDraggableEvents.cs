using System;
using UnityEngine;

public interface IDraggableEvents
{
    void OnDraggingBegins(object sender, GameObject selectedObject);
    void OnDraggingEnds(object sender, EventArgs e);
    void OnLineOfSightBlocked(object sender, PlayerItemDragger.LineOfSightArgs lineOfSightArgs);
    void OnLineOfSightUnblocked(object sender, EventArgs e);

}