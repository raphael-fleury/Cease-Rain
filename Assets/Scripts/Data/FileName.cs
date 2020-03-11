using System;
using System.Collections.Generic;
using System.IO;

public class FileName
{
    string name;

    public FileName(string name)
    {
        if (name.Length < 1)
            throw new ArgumentException("Empty text.");
        
        foreach(string s in invalidFileNames)
        {
            if (name.Equals(s))
                throw new ArgumentException("Invalid name.");
        }

        foreach (char ch in Path.GetInvalidFileNameChars())
        {
            if (name.Contains(ch.ToString()))
                throw new ArgumentException("Invalid characters in name.");
        }

        this.name = name;
    }

    #region Properties
    public static IEnumerable<string> invalidFileNames
    {
        get
        {
            string[] array = { "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3",
            "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2",
            "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };
            return array;
        }       
    }

    public string fullPath
    {
        get { return SaveSystem.GetFullPath(name); }
    }

    public bool fileExists
    {
        get { return SaveSystem.FileExists(name); }
    }
    #endregion

    #region Methods
    public Save GetSave()
    {
        return SaveSystem.GetSave(fullPath);
    }

    public override string ToString()
    { 
        return name; 
    }
    #endregion

    public static implicit operator string(FileName f) => f.ToString();
}
