using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    [Header("References")]
    public GameObject loadingBar;
    public Image loadFill;

    [Header("Options")]
    public SceneEnum sceneToLoad;
    public bool async = true;
    

    public void LoadScene()
    {
        int scene = (int)sceneToLoad;

        if (async)
        {
            StartCoroutine(Loading(scene));
            loadingBar.SetActive(true);
        }
        else { SceneManager.LoadScene(scene); }
    }

    IEnumerator Loading(int scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        loadFill.fillAmount = operation.progress;
        if (loadFill.fillAmount == .9f) { loadFill.fillAmount = 1f; }
        while (!operation.isDone) { yield return null; }
    }
}
