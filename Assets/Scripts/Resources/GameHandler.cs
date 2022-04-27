using UnityEngine;
using System;

public static class GameHandler
{

    public static event EventHandler OnGamePauseEvent;
    public static event EventHandler OnGameResumeEvent;
    public static bool isPaused = false;

    public static void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        OnGamePause();
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        OnGameResume();
    }

    private static void OnGamePause()
    {
        OnGamePauseEvent?.Invoke(null, EventArgs.Empty);
    }

    private static void OnGameResume()
    {
        OnGameResumeEvent?.Invoke(null, EventArgs.Empty);
    }
}