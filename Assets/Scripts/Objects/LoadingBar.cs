using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] SceneEnum sceneToLoad;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            LoadScene();
    }

    public void LoadScene()
    {
        int scene = (int)sceneToLoad;
        StartCoroutine(Loading(scene));
    }

    IEnumerator Loading(int scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        GetComponent<Image>().fillAmount = operation.progress;
        //if (loadFill.fillAmount == .9f) { loadFill.fillAmount = 1f; }
        while (!operation.isDone) { yield return null; }
    }
}
