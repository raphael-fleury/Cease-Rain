public class SaveName : FileName
{
    public SaveName(string name) : base(name) { }

    public string fullPath
    {
        get { return SaveSystem.GetFullPath(name); }
    }

    public bool fileExists
    {
        get { return SaveSystem.FileExists(name); }
    }

    public Save GetSave()
    {
        return SaveSystem.GetSave(fullPath);
    }
}
