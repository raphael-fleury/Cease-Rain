using System;
using UnityEngine.SceneManagement;

public static class Game
{
    #region Properties
    public static Language language { get; private set; }
    public static int currentScene { get; private set; }
    public static string currentSave { get; private set; }
    #endregion

    public static event Action<int> OnLanguageChange;

    #region Methods

    #region Load Scene
    public static void LoadScene(int scene)
    {
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
    public static void Save(Save save, string fileName) =>
        SaveSystem.SaveGame(save, fileName);

    public static void Save() =>
        Save(new Save(currentScene, Level.checkpoint), currentSave);
    #endregion

    public static void NewGame(string fileName)
    {
        Save(new Save(SceneEnum.Cutscene), fileName);
        LoadGame(fileName);
    }

    public static void LoadGame(string fileName)
    {
        LoadScene(new Save(fileName).level);
        currentSave = fileName;
    }

    public static void ChangeLanguage(Language language)
    {
        if(Game.language != language)
        {
            Game.language = language;
            OnLanguageChange?.Invoke((int)language);
        }
    }

    #endregion
}
