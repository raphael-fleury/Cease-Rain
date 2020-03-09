using UnityEngine.SceneManagement;

public class Game
{
    #region Fields
    public Language language;
    public static int currentScene;
    public static string currentSave;
    #endregion

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
    public static void Save(Save save) =>
        SaveSystem.SaveGame(save);

    public static void Save() =>
        Save(new Save(currentSave, currentScene, Level.checkpoint));
    #endregion
}
