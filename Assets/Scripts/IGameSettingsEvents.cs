using System;

public interface IGameSettingsEvents
{
    void OnSettingsChanged(object sender, EventArgs e); // Invoked from GameSettings.cs
}
