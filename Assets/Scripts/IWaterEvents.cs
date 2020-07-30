using System;

public interface IWaterEvents
{
    void OnWaterTriggerEnter2D(object sender, EventArgs e); // Invoked in WaterGrabber.cs
    void OnWaterTriggerExit2D(object sender, EventArgs e); // Invoked in WaterGrabber.cs
}
