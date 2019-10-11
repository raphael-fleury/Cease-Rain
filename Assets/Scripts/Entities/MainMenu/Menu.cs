﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Menu : MonoBehaviour
{
    public SceneEnum creditsScene;
    public Button loadGameButton;

    void Awake()
    {
        string path = Data.Folder();
        loadGameButton.interactable = Directory.Exists(path) && Directory.GetFiles(path).Length > 0;
    }

    public void LoadGame()
    {
        
    }

    public void Credits() { SceneManager.LoadScene((int)creditsScene); }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
