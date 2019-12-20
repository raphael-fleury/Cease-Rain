using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainGenerator : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField]
    float _rainIntensity;

    //[Space(10)]
    [Range(0,1)]
    public float minIntensity;

    ParticleSystem particles;
    AudioSource audioSource;

    public float rainIntensity
    {
        get { return  _rainIntensity; }
        set { ChangeIntensity(value); }
    }

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        ChangeIntensity(rainIntensity);
    }

    #if UNITY_EDITOR
    void Update() { ChangeIntensity(_rainIntensity); }
    #endif

    public void ChangeIntensity(float value)
    {
        if (value >= minIntensity && value <= 1)
        {           
            _rainIntensity = value;
            audioSource.volume = value;

            var emission = particles.emission;
            emission.rateOverTime = value * 1000;
        }       
    }
}
