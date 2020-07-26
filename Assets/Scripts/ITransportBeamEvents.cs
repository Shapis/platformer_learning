using System;

public interface ITransportBeamEvents
{
    void OnTransportBeamExpandBegins(object sender, EventArgs e); // Invoked from TransportBeam.cs
    void OnTransportBeamExpandEnds(object sender, EventArgs e); // Invoked from TransportBeam.cs
    void OnTransportBeamCollapseBegins(object sender, EventArgs e); // Invoked from TransportBeam.cs
    void OnTransportBeamCollapseEnds(object sender, EventArgs e); // Invoked from TransportBeam.cs
    void OnTransportBeamTriggerEnter2D(object sender, EventArgs e); // Not implemented
    void OnTransportBeamTriggerExit2D(object sender, EventArgs e); // Not implemented
}
