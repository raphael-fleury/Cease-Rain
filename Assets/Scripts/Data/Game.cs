using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Game: MonoBehaviour
{
    #region Fields
    public static int currentScene;
    public static string currentSave;
    //public static AudioMixer mixer;
    #endregion

    #region Load Scene
    public static void ReloadScene()
    {
        Level.checkpoint = 0;
        SceneManager.LoadScene(currentScene);
    }

    public static void LoadScene(int scene)
    {
        Level.checkpoint = 0;
        currentScene = scene;
        SceneManager.LoadScene(scene);
    }

    public static void LoadScene(SceneEnum scene) { LoadScene((int)scene); }
    #endregion

    #region Save Game
    public static void Save() { Data.SaveGame(currentSave, currentScene, Level.checkpoint); }

    public static void Save(string fileName)
    {
        currentSave = fileName;
        Data.SaveGame(currentSave, currentScene, Level.checkpoint);
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
