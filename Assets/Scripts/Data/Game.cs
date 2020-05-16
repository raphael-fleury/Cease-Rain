using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Game
{
    private static float timeScale = 1f;

    #region Properties   
    public static bool canPause { get; private set; } = true;
    public static bool isPaused { get; private set; } = false;
    public static string currentSave { get; private set; }

    public static int currentScene
    {
        get { return SceneManager.GetActiveScene().buildIndex; }
    }

    public static Language language
    {
        get { return language; }
        set
        {
            if (language != value)
            {
                language = value;
                onLanguageChangeEvent?.Invoke((int)language);
            }
        }
    }
    #endregion

    #region Events
    private static event Action<int> onLanguageChangeEvent;

    public static event Action<int> OnLanguageChangeEvent
    {
        add { onLanguageChangeEvent += value; }
        remove { onLanguageChangeEvent -= value; }
    }
    #endregion

    #region Methods

    #region Load Scene
    public static void LoadScene(int scene)
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(scene);        
    }

    public static void LoadScene(SceneEnum scene) =>
        LoadScene((int)scene);

    public static void ReloadScene() =>
        LoadScene(currentScene);
    #endregion

    #region Save Game
    public static void Save(Save save, SaveName name) =>
        SaveSystem.SaveGame(save, name);

    public static void Save() =>
        Save(new Save(currentScene, Level.checkpoint), new SaveName(currentSave));
    #endregion

    public static void NewGame(SaveName name)
    {
        Save(new Save(SceneEnum.Cutscene), name);
        LoadGame(name);
    }

    public static void LoadGame(SaveName name)
    {
        LoadScene(new Save(name).level);
        currentSave = name;
    }

    #region Pause
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

    #endregion
}
