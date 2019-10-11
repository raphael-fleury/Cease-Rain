using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGameButton : MonoBehaviour
{
    public GameObject loadGameUI;
    public Button deleteButton;
    public Button okButton;
    public SaveGame saveGame;

    public void ToggleMenu(bool on) { loadGameUI.SetActive(on); }

    public void SelectSave()
    {
        okButton.interactable = true;
        deleteButton.interactable = true;
    }

    public void DeleteSave()
    {
        File.Delete(Data.FullPath(saveGame.FileName));
        okButton.interactable = false;
        deleteButton.interactable = false;
        saveGame = null;
    }

    public void LoadGame()
    {
        Game.currentSave = saveGame.FileName;
        Game.LoadScene(saveGame.Level);
    }

}
