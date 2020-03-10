using UnityEngine;

public static class Level
{
    public static Marjory marjory;
    public static GameObject activeCamera;
    public static int checkpoint;

    #region Pause
    private static float timeScale = 1f;
    public static bool isPaused { get; private set; }

    public static void Pause()
    {
        timeScale = Time.timeScale;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public static void Resume()
    {
        Time.timeScale = timeScale;
        isPaused = false;
    }
    #endregion
}
