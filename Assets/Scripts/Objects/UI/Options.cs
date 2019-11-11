using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [Header("References")]
    public GameObject options;

    void Awake()
    {
        LoadResolutions();
        LoadGraphics();

        music.Load();
        sound.Load();
    }

    public void Toggle(bool on) { options.SetActive(on); }

    #region Video
    [Header("Video")]
    public bool fullscreen;

    [Space(10)]
    public Dropdown resolution;
    public Dropdown graphics;

    #region Resolution
    public void LoadResolutions()
    {        
        List<string> options = new List<string>();

        Vector2 res = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        int actual = 0;
        
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            options.Add(Screen.resolutions[i].width + " x " + Screen.resolutions[i].height);

            if (Screen.resolutions[i].width == res.x && Screen.resolutions[i].height == res.y)
                actual = i;
        }

        resolution.ClearOptions();
        resolution.AddOptions(options);
        resolution.value = actual;
        resolution.RefreshShownValue();
    }

    public void Resolution(int index)
    {
        PlayerPrefs.SetInt("Resolution", index);
        PlayerPrefs.Save();
        Resolution resolution = Screen.resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, fullscreen);
    }
    #endregion

    #region Graphics
    public void LoadGraphics()
    {
        if (PlayerPrefs.HasKey("Graphics"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Graphics"));
            graphics.SetValueWithoutNotify (PlayerPrefs.GetInt("Graphics"));
        }            
        else
        {
            PlayerPrefs.SetInt("Graphics", QualitySettings.GetQualityLevel());
            PlayerPrefs.Save();
        }
    }

    public void Graphics(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("Graphics", index);
        PlayerPrefs.Save();
    }
    #endregion

    #endregion

    #region Audio
    [Header("Audio")]
    public AudioOption music;
    public AudioOption sound;

    [System.Serializable]
    public class AudioOption
    {
        public string option;
        public Text amount;
        public Slider slider;

        public void Load()
        {
            if (!PlayerPrefs.HasKey(option))
                Update(60);
            else {
                float f = PlayerPrefs.GetFloat(option, 60);
                Update(f);
                slider.value = f;
            }
                            
        }

        public void Update(float f)
        {
            PlayerPrefs.SetFloat(option, f);
            PlayerPrefs.Save();

            amount.text = "" + Mathf.RoundToInt(f / slider.maxValue * 100);
        }
    }

    public void MusicVolume(float f) { music.Update(f); }
    public void AudioVolume(float f) { sound.Update(f); }
    #endregion
}
