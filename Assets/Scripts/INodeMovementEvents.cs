using System.Collections.Generic;
using UnityEngine;

public interface INodeMovementEvents
{
    void OnInitialDestinationNodeLoaded(object sender, GameObject nodeInfo);
    // Invoked from PlayerNodeMovement.cs
    void OnTravelNodeReached(object sender, GameObject nodeInfo); // Invoked from PlayerNodeMovement.cs
    void OnTravelNodeDeparted(object sender, GameObject nodeInfo); // Invoked from PlayerNodeMovement.cs
    void OnDestinationNodeReached(object sender, GameObject nodeInfo); // Invoked from PlayerNodeMovement.cs
    void OnDestinationNodeDeparted(object sender, GameObject nodeInfo); // Invoked from PlayerNodeMovement.cs
    void OnDestinationNotAccessible(object sender, GameObject nodeInfo); // Invoked from PlayerNodeMovement.cs
    void OnNoDestinationFound(object sender, string s); // Invoked from PlayerNodeMovement.cs

}
