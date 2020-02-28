using UnityEngine;

public class RainGenerator : MonoBehaviour
{
    #region Fields    
    ParticleSystem particles;
    AudioSource audioSource;

    [Range(0,1)]
    [SerializeField] float _rainIntensity;

    //[Space(10)] 
    [SerializeField] [Range(0,1)] float minIntensity;
    #endregion

    #region Properties
    public float rainIntensity
    {
        get { return  _rainIntensity; }
        set { ChangeIntensity(value); }
    }
    #endregion

    #region Methods
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
    #endregion
}
