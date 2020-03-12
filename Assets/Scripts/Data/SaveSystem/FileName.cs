using System;
using System.Collections.Generic;
using System.IO;

public class FileName
{
    protected string name;

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

    public override string ToString()
    { 
        return name; 
    }

    public static implicit operator string(FileName f) => f.ToString();
}
