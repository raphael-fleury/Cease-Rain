using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marjory : Character
{
    Movement movement;

    [Range(0, 100)]
    public float toxicity;

    public Animator mechArm;

    #region Shooting
    [Header("Shooting")]
    public Gun[] guns;
    public enum Guns { None, Umbrella, Codomoon, Footloose, Elvisnator, WordShooter, Crossline }
    public Guns currentGun;
    public float recharging;

    public void SetGun(int gun, int bullets)
    {
        if ((int)currentGun == gun)
        {
            guns[gun].bullets += bullets;
            return;
        }

        if (guns[(int)currentGun])
        {
            //Debug.Log("Desativando " + guns[(int)currentGun]);
            guns[(int)currentGun].gameObject.SetActive(false);
        }

        if (guns[gun])
        {
            //Debug.Log("Ativando " + guns[gun]);
            guns[gun].gameObject.SetActive(true);
            guns[gun].bullets = bullets;
        }

        currentGun = (Guns)gun;
        mechArm.SetInteger("gun", gun);
    }

    public void SetGun(Guns gun, int bullets)
    {
        SetGun((int)gun, bullets);
    }
    #endregion

    #region Unity Functions
    void Awake()
    {
        Level.marjory = this;
        movement = GetComponent<Movement>();
    }

    private void FixedUpdate()
    {
        if (toxicity > 0)
            toxicity -= 0.02f;
        if (toxicity > 20)
            life -= 0.02f;
        if (recharging > 0) { recharging -= 0.02f; }
    }

    protected override void Update()
    {
        base.Update();
        //HUD.canvas.transform.localScale = movement.Flip();

        //Shoot
        if (Input.GetKey(Controls.FindKey("ShootKey")) && guns[(int)currentGun] && recharging <= 0)
            guns[(int)currentGun].Shoot();

        mechArm.SetBool("diagonal", Input.GetKey(Controls.FindKey("DiagonalAimKey")));

        #if UNITY_EDITOR
        ChangeGun();
        #endif
    }
    #endregion

    protected override void Death() { }

    #region Tests
    private void ChangeGun()
    {
        int aux = (int)KeyCode.Alpha1;
        for (int i = aux; i < aux + 7; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                //Debug.Log((KeyCode)i + " " + (Guns)(i - aux));
                SetGun(i - aux, 60);
            }
        }
    }
    #endregion
}
