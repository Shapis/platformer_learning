using UnityEngine;

public static class GameHandler {
    public static bool isPaused = false;

    public static void Pause () {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public static void Resume () {
        Time.timeScale = 1f;
        isPaused = false;
    }
}