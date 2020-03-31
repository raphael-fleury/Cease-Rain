using UnityEngine;

[RequireComponent(typeof(MarjoryShooting), typeof(MarjoryMovement), typeof(Feet))]
public class Marjory : Character
{
    #region Fields
    MarjoryShooting shooting;
    MarjoryMovement movement;
    Feet feet;

    [Range(0, 100)] public float toxicity;

    [Header("References")]
    [SerializeField] GameObject interactionIcon;
    #endregion

    public enum Guns { None, Umbrella, Codomoon, Footloose, Elvisnator, WordShooter, Crossline }

    #region Properties
    public bool controllable
    {
        set
        {
            shooting.canShoot = value;
            movement.canMove = value;
            feet.canJump = value;
        }
    }

    public bool interactionIconActive
    {
        set { interactionIcon.SetActive(value); }
    }
    #endregion

    #region Methods
    public void SetGun(Guns gun, int bullets) =>
       shooting.SetGun((int)gun, bullets);
  
    void Awake()
    {
        Level.marjory = this;
        feet = GetComponent<Feet>();
        movement = GetComponent<MarjoryMovement>();
        shooting = GetComponent<MarjoryShooting>();
    }

    void FixedUpdate()
    {
        if (toxicity > 0)
            toxicity -= Time.fixedDeltaTime / 2;
        if (toxicity > 20)
            life -= Time.fixedDeltaTime;
    }

    void OnParticleCollision(GameObject other) =>
        toxicity += 0.2f;

    protected override void Death() {}
    #endregion
}
