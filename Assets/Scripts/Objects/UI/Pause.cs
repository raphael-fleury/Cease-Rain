using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseUI;

    void Update()
    {
        if (Input.GetKeyDown(Controls.FindKey("PauseKey")))
        {
            if (!Level.isPaused) { PauseGame(); }
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
        Level.Resume();

        Game.Save();
        SceneManager.LoadScene((int)SceneEnum.Menu);
    }
}
