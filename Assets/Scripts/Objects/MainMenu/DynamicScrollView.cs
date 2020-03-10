﻿using UnityEngine;
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
    #endregion

    #region Public Methods
    public void OnButtonClick(int index)
    {
        Button[] buttons = container.gameObject.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = i != index;
        }

        selectedSave = index;

        okButton.interactable = true;
        deleteButton.interactable = true;
    }

    public void DeleteSave()
    {
        File.Delete(files[selectedSave]);
        okButton.interactable = false;
        deleteButton.interactable = false;
        //selectedSave = null;
        Awake();
    }

    public void LoadGame()
    {
        Game.LoadGame(selectedSaveName);
    }
    #endregion

    string SaveDisplay(string path)
    {
        string fileName = Path.GetFileNameWithoutExtension(path);
        string display = fileName + " -";

        Save save = SaveSystem.GetSave(path);

        string level = ((SceneEnum)save.level).ToString();
        foreach (string txt in Regex.Split(level, @"(?<!^)(?=[A-Z])"))
            display += " " + txt;

        return display + "\n" + File.GetLastWriteTime(path);
    }

    void Awake()
    {
        files = SaveSystem.files.ToList();

        container.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            prefab.GetComponent<RectTransform>().sizeDelta.y * files.Count
        );

        //Sorting files by last write time
        files.Sort((f1, f2) => File.GetLastWriteTime(f1).CompareTo(File.GetLastWriteTime(f2)));

        saves = new List<Save>();
        foreach(string file in files)
            saves.Add(SaveSystem.GetSave(file));            

        for (int i = 0; i < saves.Count; i++)
        {
            GameObject go = Instantiate(prefab);
            go.GetComponentInChildren<Text>().text = SaveDisplay(files[i]);
            go.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(i));

            go.transform.SetParent(container);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;           
        }
    }
}