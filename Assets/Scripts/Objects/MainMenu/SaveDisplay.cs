using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SaveDisplay : MonoBehaviour
{
    SaveListUI scrollView;
    Button button;

    public SaveName saveName { get; private set; }

    public void Load(SaveListUI saveList, SaveName name)
    {
        scrollView = saveList;
        saveName = name;
        Save save = name.GetSave();
        button.GetComponentInChildren<Text>().text = Display();
    }

    public void OnButtonClick() =>
        scrollView.OnButtonClick(this);

    void Awake() =>
        button = GetComponent<Button>();

    string Display()
    {
        Save save = saveName.GetSave();

        string display = saveName + " -";
        string level = ((SceneEnum)save.level).ToString();

        foreach (string txt in Regex.Split(level, @"(?<!^)(?=[A-Z])"))
            display += " " + txt;

        return display + "\n" + File.GetLastWriteTime(saveName.fullPath);
    }
}
