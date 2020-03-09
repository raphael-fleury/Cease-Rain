using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class DynamicScrollView : MonoBehaviour
{
    #region Fields
    [Header("Status")]
    [SerializeField] Save selectedSave;
    [SerializeField] List<Save> files = new List<Save>();

    [Header("References")]
    [SerializeField] GameObject prefab;

    [Space(10)]
    [SerializeField] Button deleteButton;
    [SerializeField] Button okButton;
    [SerializeField] Transform container;
    #endregion

    #region Public Methods
    public void OnButtonClick(int index)
    {
        Button[] buttons = container.gameObject.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = i != index;
        }

        selectedSave = files[index];

        okButton.interactable = true;
        deleteButton.interactable = true;
    }

    public void DeleteSave()
    {
        File.Delete(SaveSystem.GetFullPath(selectedSave.fileName));
        okButton.interactable = false;
        deleteButton.interactable = false;
        selectedSave = null;
        Awake();
    }

    public void LoadGame()
    {
        Game.currentSave = selectedSave.fileName;
        Game.LoadScene(selectedSave.level);
    }
    #endregion

    string SaveDisplay(Save save)
    {
        string text = save.fileName + " -";
        string[] level = Regex.Split(((SceneEnum)save.level).ToString(), @"(?<!^)(?=[A-Z])");
        foreach (string txt in level)
            text += " " + txt;
        return text + "\n" + File.GetLastWriteTime(SaveSystem.GetFullPath(save.fileName));
    }

    void Awake()
    {        
        string[] fileList = Directory.GetFiles(SaveSystem.folder);

        container.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            prefab.GetComponent<RectTransform>().sizeDelta.y * fileList.Length
        );

        files = new List<Save>();
        foreach (string f in fileList)
            files.Add(SaveSystem.GetSave(Path.GetFileNameWithoutExtension(f)));

        //Sorting saves by write time
        files.Sort((f1, f2) => File.GetLastWriteTime(SaveSystem.GetFullPath(f1.fileName)).CompareTo(File.GetLastWriteTime(SaveSystem.GetFullPath(f2.fileName))));

        for (int i = 0; i < files.Count; i++)
        {
            GameObject go = Instantiate(prefab);
            go.GetComponentInChildren<Text>().text = SaveDisplay(files[i]);
            go.transform.SetParent(container);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            int buttonIndex = i;
            go.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }
}