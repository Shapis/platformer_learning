using System;

public interface IGameHandlerEvents
{
    void OnGamePause(object sender, EventArgs e); // Invoked from GameHandler.cs
    void OnGameResume(object sender, EventArgs e); // Invoked from GameHandler.cs
}