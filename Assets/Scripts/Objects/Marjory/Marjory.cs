﻿using UnityEngine;

[RequireComponent(typeof(MarjoryShooting), typeof(MarjoryMovement), typeof(Feet))]
public class Marjory : Character
{
    #region Fields
    MarjoryShooting shooting;
    MarjoryMovement movement;
    Feet feet;

    [SerializeField, Range(0, 100)] public float _toxicity;

    [Header("References")]
    [SerializeField] GameObject interactionIcon;
    [SerializeField] MarjoryDefense normalArm;

    bool _controllable = true;
    #endregion

    public enum Guns { None, Umbrella, Codomoon, Footloose, Elvisnator, WordShooter, Crossline }

    #region Properties
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
            controllable = value;
            normalArm.canDefend = value;
            shooting.canShoot = value;
            movement.canMove = value;
            feet.canJump = value;
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
            movement.face.SetBool("eyesClosed", value);
            controllable = !value;
        }
    }
    #endregion

    #region Methods
    public void SetGun(Guns gun, int bullets) =>
       shooting.SetGun((int)gun, bullets);
  
    protected override void Awake()
    {
        base.Awake();
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Umbrella")
            normalArm.canDefend = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Umbrella")
            normalArm.canDefend = false;
    }

    protected override void Death() {}
    #endregion
}
