using UnityEngine;

public class Marjory : Character
{
    #region Fields
    MarjoryShooting shooting;
    MarjoryMovement movement;
    Feet feet;

    [Range(0, 100)] public float toxicity;
    #endregion

    public enum Guns { None, Umbrella, Codomoon, Footloose, Elvisnator, WordShooter, Crossline }

    #region Public Methods
    public void Freeze()
    {
        movement.canMove = false;
        feet.canJump = false;
    }

    public void Unfreeze()
    {
        movement.canMove = true;
        feet.canJump = true;
    }

    public void SetGun(Guns gun, int bullets) => 
        shooting.SetGun((int)gun, bullets);
    #endregion

    #region Private Methods
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

    //protected override void Death() {}
    #endregion
}
