using System;

public interface IChestGrabberEvents
{
    void OnChestOpened(object sender, EventArgs e); // Invoked in ChestGrabber.cs
}
