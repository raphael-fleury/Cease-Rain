using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DynamicScrollView : MonoBehaviour
{
    public LoadGameButton loadMenu;
    public GameObject Prefab;
    public Transform Container;
    public List<SaveGame> files = new List<SaveGame>();

    string[] fileList;

    void Awake()
    {
        fileList = Directory.GetFiles(Data.Folder());
        float size = Prefab.GetComponent<RectTransform>().sizeDelta.y * fileList.Length;
        Container.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
    }

    void Start()
    {        
        Debug.Log(fileList.Length);
        for (int i = 0; i < fileList.Length; i++)
        {            
            fileList[i] = Path.GetFileNameWithoutExtension(fileList[i]);
            Debug.Log(fileList[i]);
            files.Add(Data.LoadGame(fileList[i]));
        }

        files.Sort((f1, f2) => File.GetLastWriteTime(Data.FullPath(f1.FileName)).CompareTo(File.GetLastWriteTime(Data.FullPath(f2.FileName))));

        for (int i = 0; i < files.Count; i++)
        {
            GameObject go = Instantiate(Prefab);
            go.GetComponentInChildren<Text>().text = SaveDisplay(files[i]);
            go.transform.SetParent(Container);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            int buttonIndex = i;
            go.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    public string SaveDisplay(SaveGame save)
    {
        string text = save.FileName + " -";
        string[] level = Regex.Split(((SceneEnum)save.Level).ToString(), @"(?<!^)(?=[A-Z])");
        foreach(string txt in level) { text += " " + txt; }
        return text + "\n" + File.GetLastWriteTime(Data.FullPath(save.FileName));
    }

    public void OnButtonClick(int index) {
        Button[] buttons = Container.gameObject.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = i != index ? true : false;
        }

        loadMenu.saveGame = files[index];
    }

}