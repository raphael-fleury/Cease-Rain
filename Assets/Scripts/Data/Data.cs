using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveGame
{
    public string FileName;
    public int Level = 0;
    public int Checkpoint;

    public SaveGame(string fileName, int level, int checkpoint)
    {
        FileName = fileName;
        Level = level;
        Checkpoint = checkpoint;
    }
}

public class Data : MonoBehaviour
{
    #region String Methods
    public static string Folder() { return Application.persistentDataPath + "/savegames"; }
    public static string Format() { return ".sav"; }
    public static string FullPath(string fileName) { return Folder() + "/" + fileName + Format(); }

    public static string[] Files() { return Directory.GetFiles(Folder()); }
    #endregion

    #region Bool Methods
    public static bool FolderExists() { return Directory.Exists(Folder()); }
    public static bool FileExists(string fileName) { return File.Exists(FullPath(fileName)); }
    #endregion

    #region Save Game
    public static void SaveGame(string fileName, int level, int checkpoint)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = FullPath(fileName);

        if (!FolderExists())
            Directory.CreateDirectory(Folder());

        if (FileExists(fileName))
            File.Delete(path);

        FileStream stream = new FileStream(path, FileMode.Create);
        SaveGame data = new SaveGame(fileName, level, checkpoint);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveGame(string fileName, SceneEnum level, int checkpoint) =>
        SaveGame(fileName, (int)level, checkpoint);

    public static void SaveGame(SaveGame saveGame) =>
        SaveGame(saveGame.FileName, saveGame.Level, saveGame.Checkpoint);
    #endregion

    #region Load Game
    public static SaveGame LoadGame(string fileName)
    {
        string path = FullPath(fileName);
        Debug.Log(path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveGame data = formatter.Deserialize(stream) as SaveGame;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + Folder());
            return null;
        }
    }
    #endregion
}