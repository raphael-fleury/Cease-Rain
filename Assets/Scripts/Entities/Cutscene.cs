using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour {

    VideoPlayer video;

    public LoadingBar loader;

    [Header("Skip")]
    public KeyCode skipButton;
    public Text textSkip;

    [Space(10)]
    public Color white;
    public Color grey;

    void Awake() { video = GetComponent<VideoPlayer>(); }
    void Start() { Invoke("LoadScene", (float)video.length); }

    void LoadScene() {
        loader.LoadScene();
        textSkip.enabled = false;
    }

    void Update() {
        if (Input.GetKeyDown(skipButton)) { LoadScene(); }
    }

    void FixedUpdate() { textSkip.color = Color.Lerp(white, grey, Mathf.PingPong(Time.time, 1)); }

}
