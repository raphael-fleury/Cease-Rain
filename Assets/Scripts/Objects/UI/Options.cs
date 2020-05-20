using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Options : MonoBehaviour
{
    #region Fields
    GameObject origin;

    [Header("Video")]
    [SerializeField] bool fullscreen;

    [Space(10)]
    [SerializeField] Dropdown resolution;
    [SerializeField] Dropdown graphics;

    [Header("Audio")]
    [SerializeField] AudioMixer  mixer;
    [SerializeField] AudioOption music;
    [SerializeField] AudioOption sound;
    #endregion

    #region Events
    private static Action<Resolution> onResolutionChange;

    public static event Action<Resolution> OnResolutionChange
    {
        add { onResolutionChange += value; }
        remove { onResolutionChange -= value; }
    }
    #endregion

    public void Enable(GameObject origin)
    {
        this.origin = origin;
        gameObject.SetActive(true);
    }

    #region Video

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
        Resolution resolution = Screen.resolutions[index];
        PlayerPrefs.SetInt("Resolution", index);
        PlayerPrefs.Save();       
        Screen.SetResolution(resolution.width, resolution.height, fullscreen);
        onResolutionChange?.Invoke(resolution);
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
    public void MusicVolume(float f) { music.Update(mixer, f); }
    public void AudioVolume(float f) { sound.Update(mixer, f); }

    [System.Serializable]
    public class AudioOption
    {
        public string option;
        public Text amount;
        public Slider slider;

        public void Load(AudioMixer mixer)
        {
            if (!PlayerPrefs.HasKey(option))
                Update(mixer, 60);
            else
            {
                float f = PlayerPrefs.GetFloat(option, 60);
                Update(mixer, f);
                slider.value = f;
                mixer.SetFloat(option, f - 80);
            }

        }

        public void Update(AudioMixer mixer, float f)
        {
            PlayerPrefs.SetFloat(option, f);
            PlayerPrefs.Save();

            amount.text = "" + Mathf.RoundToInt(f / slider.maxValue * 100);
            mixer.SetFloat(option, f - 80);
        }
    }
    #endregion

    #region Private Methods
    void OnEnable()
    {
        LoadResolutions();
        LoadGraphics();

        music.Load(mixer);
        sound.Load(mixer);
    }

    void OnDisable() =>
        origin?.SetActive(true);
    #endregion

}
