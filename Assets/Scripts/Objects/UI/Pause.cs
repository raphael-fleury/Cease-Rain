using UnityEngine;

public class Pause : LevelScreen
{
    [SerializeField] GameObject pauseUI;

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

    void Update()
    {
        if (Input.GetKeyDown(Controls.FindKey("PauseKey")) && Game.canPause)
        {
            if (!Game.isPaused)
                PauseGame();
            else
                Resume();
        }
    }
}
