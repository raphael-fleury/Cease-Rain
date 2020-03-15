using UnityEngine;

public class LevelScreen : MonoBehaviour
{
    [SerializeField] Options options;

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

    public void OpenOptions()
    {
        gameObject.SetActive(false);
        options.Enable(gameObject);
    }
}
