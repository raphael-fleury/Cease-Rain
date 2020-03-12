using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class SaveListUI : MonoBehaviour
{
    #region Fields
    [Header("Status")]
    [SerializeField] SaveDisplay selectedSave;

    [Header("References")]
    [SerializeField] GameObject prefab;

    [Space(10)]
    [SerializeField] Button deleteButton;
    [SerializeField] Button okButton;
    [SerializeField] Transform container;
    #endregion

    #region Public Methods
    public void OnButtonClick(SaveDisplay display)
    {
        selectedSave = display;
        SetButtonsActive(true);
    }

    public void DeleteSave()
    {
        File.Delete(selectedSave.saveName.fullPath);
        OnEnable();
    }

    public void LoadGame() =>
        Game.LoadGame(selectedSave.saveName);
    #endregion

    void SetButtonsActive(bool value)
    {
        okButton.interactable = value;
        deleteButton.interactable = value;
    }

    void OnEnable()
    {
        List<string> files = SaveSystem.files.ToList();

        SetButtonsActive(false);

        //Destroy(buttons);
        //GameObject buttons = Instantiate(buttons);
        //buttons.transform.parent = container;

        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        container.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            prefab.GetComponent<RectTransform>().sizeDelta.y * files.Count
        );

        //Sorting files by last write time
        files.Sort((f1, f2) => 
            File.GetLastWriteTime(f1).CompareTo(File.GetLastWriteTime(f2)));     

        foreach(string path in files)
        {
            Debug.Log(path);
            GameObject go = Instantiate(prefab);

            go.GetComponent<SaveDisplay>().Load(this, new SaveName(Path.GetFileNameWithoutExtension(path)));

            go.transform.SetParent(container);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
        }
    }
}