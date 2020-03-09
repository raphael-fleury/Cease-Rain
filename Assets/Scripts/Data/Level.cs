using UnityEngine;

public class Level : MonoBehaviour
{
    public static Marjory marjory;
    public static GameObject activeCamera;
    public static int checkpoint;

    #region Pause
    private static float timeScale = 1f;
    public static bool paused;

    public static void Pause()
    {
        timeScale = Time.timeScale;
        Time.timeScale = 0f;
        paused = true;
    }

    public static void Resume()
    {
        Time.timeScale = timeScale;
        paused = false;
    }
    #endregion
}
