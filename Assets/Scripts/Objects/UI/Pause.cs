using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseUI;

    void Update()
    {
        if (Input.GetKeyDown(Controls.FindKey("PauseKey")))
        {
            if (!Level.paused) { PauseGame(); }
            else { Resume(); }
        }
    }

    public void PauseGame()
    {
        Level.Pause();
        pauseUI.SetActive(true);
    }

    public void Resume()
    {
        Level.Resume();
        pauseUI.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Game.ReloadScene();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Level.paused = false;

        Game.Save();
        SceneManager.LoadScene((int)SceneEnum.Menu);
    }
}
