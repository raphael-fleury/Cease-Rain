using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Menu : MonoBehaviour
{
    [SerializeField] SceneEnum creditsScene;
    [SerializeField] Button loadGameButton;

    void Awake()
    {
        string path = SaveSystem.folder;
        loadGameButton.interactable = Directory.Exists(path) && Directory.GetFiles(path).Length > 0;
    }

    public void Credits() =>
        SceneManager.LoadScene((int)creditsScene);

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
