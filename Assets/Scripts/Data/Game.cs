using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Game
{
    private static float timeScale = 1f;

    #region Properties
    public static Language language { get; private set; }
    public static int currentScene { get; private set; }
    public static string currentSave { get; private set; }
    public static bool isPaused { get; private set; }
    #endregion

    public static event Action<int> OnLanguageChange;

    #region Methods

    #region Load Scene
    public static void LoadScene(int scene)
    {
        isPaused = false;
        Time.timeScale = 1f;
        Level.checkpoint = 0;
        currentScene = scene;
        SceneManager.LoadScene(scene);        
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

    public static void ChangeLanguage(Language language)
    {
        if(Game.language != language)
        {
            Game.language = language;
            OnLanguageChange?.Invoke((int)language);
        }
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
