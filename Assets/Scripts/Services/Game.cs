using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game: MonoBehaviour
{
    #region Fields
    public static int currentScene;
    public static int checkpoint;
    public static string currentSave;
    public static bool paused;

    private static float timeScale = 1f;
    #endregion

    #region Pause
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

    #region Load Scene
    public static void ReloadScene()
    {
        checkpoint = 0;
        SceneManager.LoadScene(currentScene);
    }

    public static void LoadScene(int scene)
    {
        checkpoint = 0;
        currentScene = scene;
        SceneManager.LoadScene(scene);
    }

    public static void LoadScene(SceneEnum scene) { LoadScene((int)scene); }
    #endregion

    #region Save Game
    public static void Save() { Data.SaveGame(currentSave, currentScene, checkpoint); }

    public static void Save(string fileName)
    {
        currentSave = fileName;
        Data.SaveGame(currentSave, currentScene, checkpoint);
    }

    public static void Save(string fileName, int scene)
    {
        currentSave = fileName;
        Data.SaveGame(currentSave, scene, 0);
    }

    public static void Save(string fileName, SceneEnum scene)
    {
        currentSave = fileName;
        Data.SaveGame(currentSave, scene, 0);
    }
    #endregion
}
