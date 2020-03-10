using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Save
{
    public int level { get; private set; } = 0;
    public int checkpoint { get; private set; } = 0;

    #region Constructors
    public Save(int level, int checkpoint)
    {
        this.level = level;
        this.checkpoint = checkpoint;
    }

    public Save(SceneEnum scene, int checkpoint) :
        this((int)scene, checkpoint) { }

    public Save(int level) : 
        this(level, 0) { }

    public Save(SceneEnum level) : 
        this(level, 0) { }

    public Save(string fileName) =>
        Load(fileName);
    #endregion

    #region Methods
    public void Load(string fileName)
    {
        if (!SaveSystem.FileExists(fileName))
            throw new ArgumentException("File " + fileName + " does not exist.");

        Save save = SaveSystem.GetSave(SaveSystem.GetFullPath(fileName));
        level = save.level;
        checkpoint = save.checkpoint;
    }

    public void SaveGame(string fileName) => 
        SaveSystem.SaveGame(this, fileName);
    #endregion
}

public static class SaveSystem
{
    #region Properties
    public static string folder
    {
        get { return Application.persistentDataPath + "/savegames"; }
    }

    public static string format
    {
        get { return ".sav"; }
    }

    public static string[] files
    {
        get { return Directory.GetFiles(folder); }
    }

    public static bool folderExists
    {
        get { return Directory.Exists(folder); }
    }
    #endregion

    #region Methods
    public static string GetFullPath(string fileName)
    { 
        return folder + "/" + fileName + format; 
    }
    
    public static bool FileExists(string fileName)
    { 
        return File.Exists(GetFullPath(fileName)); 
    }

    public static Save GetSave(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Save data = formatter.Deserialize(stream) as Save;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + folder);
            return null;
        }
    }

    public static void SaveGame(Save save, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        foreach (char ch in Path.GetInvalidFileNameChars())
        {
            if (fileName.Contains(ch.ToString()))
                throw new ArgumentException("Invalid characters in name.");
        }

        if (fileName.Length < 1)
            throw new ArgumentException("Empty text.");

        if (!folderExists)
            Directory.CreateDirectory(folder);

        string path = GetFullPath(fileName);
        if (FileExists(fileName))
            File.Delete(path);

        FileStream stream = new FileStream(path, FileMode.Create);
        Save data = new Save(save.level, save.checkpoint);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    #endregion
}