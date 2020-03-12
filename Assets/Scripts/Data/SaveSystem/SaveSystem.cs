using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

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

    public Save(Save save) :
        this(save.level, save.checkpoint) { }

    public Save(SceneEnum scene, int checkpoint) :
        this((int)scene, checkpoint) { }

    public Save(int level) : 
        this(level, 0) { }

    public Save(SceneEnum level) : 
        this(level, 0) { }

    public Save(SaveName name) =>
        Load(name);
    #endregion

    #region Methods
    public void Load(SaveName saveName)
    {
        if (!saveName.fileExists)
            throw new ArgumentException("File " + saveName + " does not exist.");

        Save save = SaveSystem.GetSave(SaveSystem.GetFullPath(saveName));
        level = save.level;
        checkpoint = save.checkpoint;
    }

    public void SaveGame(SaveName fileName) => 
        SaveSystem.SaveGame(this, fileName);
    #endregion
}

public static class SaveSystem
{
    #region Properties
    public static string folder
    {
        get { return Application.persistentDataPath + "/saves"; }
    }

    public static string format
    {
        get { return ".sav"; }
    }

    public static string[] files
    {
        get { return Directory.GetFiles(folder); }
    }

    public static FileName[] fileNames
    {
        get 
        {
            return files.Select(f => new FileName(f)).ToArray();
        }
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

    public static void SaveGame(Save save, SaveName name)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!folderExists)
            Directory.CreateDirectory(folder);

        if (name.fileExists)
            File.Delete(name.fullPath);

        FileStream stream = new FileStream(name.fullPath, FileMode.Create);
        Save data = new Save(save);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    #endregion
}