using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

public class DynamicScrollView : MonoBehaviour
{
    #region Fields
    [Header("Status")]
    [SerializeField] int selectedSave;
    [SerializeField] List<string> files = new List<string>();
    [SerializeField] List<Save> saves = new List<Save>();
    [SerializeField] List<Button> buttons = new List<Button>();

    [Header("References")]
    [SerializeField] GameObject prefab;

    [Space(10)]
    [SerializeField] Button deleteButton;
    [SerializeField] Button okButton;
    [SerializeField] Transform container;
    #endregion

    #region Properties
    string selectedSavePath
    {
        get
        {
            return files[selectedSave];
        } 
    }

    string selectedSaveName
    {
        get
        {
            return Path.GetFileNameWithoutExtension(files[selectedSave]);
        }
    }

    string selectedSaveDisplay
    {
        get
        {
            Save save = saves[selectedSave];

            string display = selectedSaveName + " -";
            string level = ((SceneEnum)save.level).ToString();

            foreach (string txt in Regex.Split(level, @"(?<!^)(?=[A-Z])"))
                display += " " + txt;

            return display + "\n" + File.GetLastWriteTime(selectedSavePath);
        }
    }
    #endregion

    #region Public Methods
    public void OnButtonClick(int index)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Debug.Log(i + " " + (i != index).ToString() + " " + index);
            buttons[i].interactable = i != index;
        }

        selectedSave = index;

        okButton.interactable = true;
        deleteButton.interactable = true;
    }

    public void DeleteSave()
    {
        File.Delete(files[selectedSave]);
        OnEnable();
    }

    public void LoadGame() =>
        Game.LoadGame(selectedSaveName);
    #endregion

    void OnEnable()
    {
        #region Reset
        buttons = new List<Button>();
        saves = new List<Save>();
        files = SaveSystem.files.ToList();

        okButton.interactable = false;
        deleteButton.interactable = false;

        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);

        container.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            prefab.GetComponent<RectTransform>().sizeDelta.y * files.Count
        );
        #endregion

        //Sorting files by last write time
        files.Sort((f1, f2) => 
            File.GetLastWriteTime(f1).CompareTo(File.GetLastWriteTime(f2)));
       
        foreach(string file in files)
            saves.Add(SaveSystem.GetSave(file));            

        for (int i = 0; i < saves.Count; i++)
        {
            GameObject go = Instantiate(prefab);

            selectedSave = i;
            go.GetComponentInChildren<Text>().text = selectedSaveDisplay;
            go.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(i));

            go.transform.SetParent(container);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;

            buttons.Add(go.GetComponent<Button>());
        }
    }
}