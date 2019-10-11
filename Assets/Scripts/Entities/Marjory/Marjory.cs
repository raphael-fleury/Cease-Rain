using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marjory : Character
{
    Movement movement;
    Animator anim;

    [Range(0,100)]
    public float toxicity;

    #region Shooting
    [Header("Shooting")]
    public Gun[] guns;
    public enum Guns { None, Umbrella, Crossline, WordShooter, Footloose, Elvisnator, Codomoon }
    public Guns gun;  
    public float recharging;
    #endregion

    void Awake()
    {
        movement = GetComponent<Movement>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        toxicity -= 0.02f;
        if (toxicity > 20)
            life -= 0.02f;
        if (recharging > 0) { recharging -= 0.02f; }
    }

    protected override void Update()
    {
        base.Update();
        movement.axisX = Input.GetAxis("Horizontal");
        if (movement.axisX != 0)
            movement.direction = (int)Mathf.Sign(movement.axisX);
            
            //HUD.canvas.transform.localScale = movement.Flip();

        if (Input.GetKey(Controls.FindKey("ShootKey")) && (int)gun > 1 && recharging <= 0)
            guns[(int)gun - 2].Shoot(Input.GetKey(Controls.FindKey("DiagonalAimKey")));
    }

    protected override void Death() { }


}
