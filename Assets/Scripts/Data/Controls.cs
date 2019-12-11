using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key
{
    public string Name { get; set; }
    public KeyCode DefaultKey { get; set; }
    public KeyCode ChoosenKey { get; set; }

    public Key(string name, KeyCode defaultKey)
    {
        Name = name;
        DefaultKey = defaultKey;
        ChoosenKey = defaultKey;
    }

    public void Load()
    {
        ChoosenKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(Name, DefaultKey.ToString()));
    }

    public void Save()
    {
        PlayerPrefs.SetString(Name, ChoosenKey.ToString());
        PlayerPrefs.Save();
    }
}

public class Controls : MonoBehaviour
{
    //Used for singleton
    public static Controls Controller;

    //Create Keycodes that will be associated with each of our commands.
    //These can be accessed by any other script in our game
    public static List<Key> KeyList = LoadControls();

    void Awake()
    {
        if (Controller == null)
        {
            DontDestroyOnLoad(gameObject);
            Controller = this;
        }
        else if (Controller != this)
        {
            Destroy(gameObject);
        }

        KeyList = LoadControls();
    }

    private static List<Key> DefaultKeyList()
    {
        List<Key> keylist = new List<Key>();
        keylist.Add(new Key("JumpKey", KeyCode.Space));
        keylist.Add(new Key("ShootKey", KeyCode.Z));
        keylist.Add(new Key("DefendKey", KeyCode.X));
        keylist.Add(new Key("DiagonalAimKey", KeyCode.UpArrow));
        keylist.Add(new Key("InteractionKey", KeyCode.DownArrow));
        keylist.Add(new Key("PauseKey", KeyCode.P));
        return keylist;
    }

    public static List<Key> LoadControls()
    {
        List<Key> keylist = DefaultKeyList();
        foreach (Key key in keylist)
        {
            if (PlayerPrefs.HasKey(key.Name))
                key.Load();
            else
                key.Save();
        }
        return keylist;
    }

    public static void SaveControls(List<Key> keylist)
    {
        keylist.ForEach(k => k.Save());
    }

    public static KeyCode FindKey(string name) { return KeyList.Find(k => k.Name == name).ChoosenKey; }
}
