using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    #region Fields
    VideoPlayer video;

    [SerializeField] SceneEnum nextScene;

    [Header("Skip")]
    [SerializeField] KeyCode skipButton;
    [SerializeField] Text textSkip;

    [Space(10)]
    [SerializeField] Color white;
    [SerializeField] Color grey;
    #endregion

    #region Methods
    void Awake() { video = GetComponent<VideoPlayer>(); }
    void Start() { Invoke("LoadScene", (float)video.length); }

    void LoadScene()
    {
        Game.LoadScene(nextScene);
        textSkip.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(skipButton))
            LoadScene();
    }

    void FixedUpdate() { textSkip.color = Color.Lerp(white, grey, Mathf.PingPong(Time.time, 1)); }
    #endregion
}
