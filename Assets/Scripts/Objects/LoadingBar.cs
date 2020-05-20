using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    void Start() => LoadScene();

    public void LoadScene() => StartCoroutine(Loading(Game.sceneToLoad));

    IEnumerator Loading(int scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        GetComponent<Image>().fillAmount = operation.progress;
        //if (loadFill.fillAmount == .9f) { loadFill.fillAmount = 1f; }
        while (!operation.isDone) { yield return null; }
    }
}
