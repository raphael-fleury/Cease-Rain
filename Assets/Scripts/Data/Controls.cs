using System.Collections.Generic;
using UnityEngine;

public class Key
{
    public string name { get; private set; }
    public KeyCode defaultKey { get; private set; }
    public KeyCode choosenKey { get; private set; }

    public Key(string name, KeyCode defaultKey)
    {
        this.name = name;
        this.defaultKey = defaultKey;
        choosenKey = defaultKey;
    }

    public void Load()
    {
        choosenKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(name, defaultKey.ToString()));
    }
}

public class Controls
{
    #region Properties
    public static List<Key> defaultKeys
    {
        get
        {
            List<Key> keylist = new List<Key>();
            keylist.Add(new Key("JumpKey", KeyCode.Space));
            keylist.Add(new Key("ShootKey", KeyCode.Z));
            keylist.Add(new Key("DefendKey", KeyCode.X));
            keylist.Add(new Key("DiagonalAimKey", KeyCode.UpArrow));
            keylist.Add(new Key("InteractionKey", KeyCode.DownArrow));
            keylist.Add(new Key("PauseKey", KeyCode.P));
            keylist.Add(new Key("PauseKey", KeyCode.P));
            return keylist;
        }
    }

    public static List<Key> keys
    {
        get    
        {
            List<Key> keylist = defaultKeys;
            foreach (Key key in keylist)
            {
                if (PlayerPrefs.HasKey(key.name))
                    key.Load();
                else
                    SaveKey(key);
            }
            return keylist;
        }
    }
    #endregion

    #region Methods
    public static void SaveKey(Key key)
    {
        PlayerPrefs.SetString(key.name, key.choosenKey.ToString());
        PlayerPrefs.Save();
    }

    public static void SaveControls(List<Key> keylist) =>
        keylist.ForEach(k => SaveKey(k));

    public static KeyCode FindKey(string name)
    { 
        return keys.Find(k => k.name == name).choosenKey;
    }
    #endregion
}
