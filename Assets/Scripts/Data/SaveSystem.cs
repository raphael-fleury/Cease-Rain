using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Save
{
    public string fileName { get; private set; }
    public int level { get; private set; } = 0;
    public int checkpoint { get; private set; } = 0;

    #region Constructors
    public Save(string fileName, int level, int checkpoint)
    {
        foreach(char ch in Path.GetInvalidFileNameChars())
        {
            if (fileName.Contains(ch.ToString()))
                throw new ArgumentException("Invalid characters in name.");
        }
        this.fileName = fileName;
        this.level = level;
        this.checkpoint = checkpoint;
    }

    public Save(string fileName, SceneEnum scene, int checkpoint) :
        this(fileName, (int)scene, checkpoint) { }

    public Save(string fileName, int level) : 
        this(fileName, level, 0) { }

    public Save(string fileName, SceneEnum level) : 
        this(fileName, level, 0) { }
    #endregion

    public override string ToString()
    {
        return fileName + " -";
    }
}

public class SaveSystem
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

    public static Save GetSave(string fileName)
    {
        string path = GetFullPath(fileName);
        Debug.Log(path);
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

    public static void SaveGame(Save save)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetFullPath(save.fileName);

        if (!folderExists)
            Directory.CreateDirectory(folder);

        if (FileExists(save.fileName))
            File.Delete(path);

        FileStream stream = new FileStream(path, FileMode.Create);
        Save data = new Save(save.fileName, save.level, save.checkpoint);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    #endregion
}