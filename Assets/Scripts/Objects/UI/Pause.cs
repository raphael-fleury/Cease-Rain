using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseUI;

    void Update()
    {
        if (Input.GetKeyDown(Controls.FindKey("PauseKey")))
        {
            if (!Game.isPaused)
                PauseGame();
            else
                Resume();
        }
    }

    public void PauseGame()
    {
        Game.Pause();
        pauseUI.SetActive(true);
    }

    public void Resume()
    {
        Game.Resume();
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
        Game.Resume();

        Game.Save();
        Game.LoadScene(SceneEnum.Menu);
    }

}
