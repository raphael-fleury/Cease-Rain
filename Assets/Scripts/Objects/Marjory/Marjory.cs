using UnityEngine;

[RequireComponent(typeof(MarjoryShooting), typeof(MarjoryMovement), typeof(Feet))]
public class Marjory : MonoBehaviour, IMarjory
{
    #region Fields
    ILife _life;
    IJump _jump;
    IStep _step;
    MarjoryShooting _shooting;
    MarjoryMovement _movement;  

    [SerializeField, Range(0, 100)] float _toxicity;

    [Header("References")]
    [SerializeField] GameObject interactionIcon;
    [SerializeField] MarjoryDefense normalArm;

    bool _controllable = true;
    #endregion

    public enum Guns { None, Umbrella, Codomoon, Footloose, Elvisnator, WordShooter, Crossline }

    #region Properties
    public static Marjory instance { get; private set; }
    
    public ILife life => _life;
    public IJump jump => _jump;
    public IStep step => _step;

    public bool umbrellaNear { get; private set; }

    public float toxicity
    {
        get { return _toxicity; }
        set
        {
            if (value < 0)
                _toxicity = 0;
            else if (value > 100)
                _toxicity = 100;
            else
                _toxicity = value;
        }
    }

    public bool controllable
    {
        get { return _controllable; }
        set
        {
            _controllable = value;
            normalArm.canDefend = value;
            _shooting.canShoot = value;
            _movement.canMove = value;
            _jump.canPerform = value;
        }
    }

    public bool interactionIconActive
    {
        get { return interactionIcon.activeSelf; }
        set { interactionIcon.SetActive(value); }
    }

    public bool drying
    {
        set
        {
            _movement.face.SetBool("eyesClosed", value);
            controllable = !value;
        }
    }
    #endregion

    #region Methods
    public void SetGun(Guns gun, ushort bullets) =>
       _shooting.SetGun((int)gun, bullets);

    public void ResetGuns() => SetGun(Guns.Codomoon, ushort.MaxValue);
  
    void Awake()
    {
        //_life.Awake();
        instance = this;
        _life = GetComponent<CharacterLife>();
        _jump = GetComponent<SingleJump>();
        _movement = GetComponent<MarjoryMovement>();
        _shooting = GetComponent<MarjoryShooting>();
    }

    void FixedUpdate()
    {
        if (toxicity > 0)
            toxicity -= Time.fixedDeltaTime / 2;
        if (toxicity > 20)
            life.Hurt(Time.fixedDeltaTime);
    }

    void OnParticleCollision(GameObject other) =>
        toxicity += 0.2f;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Umbrella")
            umbrellaNear = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Umbrella")
            umbrellaNear = false;
    }

    void OnDestroy() => instance = null;
    #endregion
}
