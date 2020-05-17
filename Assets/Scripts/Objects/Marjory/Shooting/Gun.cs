﻿using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Fields
    protected GameObject shot;

    public Shooter shooter;
    [Header("Status")]
    [SerializeField] int _bullets;

    [Header("Options")]
    [Min(0)] public float rechargeTime;
    [SerializeField] float rotationFix;

    [Header("References")]
    [SerializeField] Marjory marjory;
    [SerializeField] AudioSource sound; 
    #endregion  

    public int bullets
    {
        get { return _bullets; }
        private set { _bullets = value; }
    }

    #region Methods
    public virtual void Shoot()
    {
        Vector2 vector = new Vector2(Mathf.Sign(Level.marjory.transform.localScale.x), Mathf.Abs(transform.rotation.z) * 2 - rotationFix);

        shot = shooter.Shoot(vector);
           
        bullets--;
        if (bullets < 1)
            marjory.SetGun(Marjory.Guns.Codomoon, int.MaxValue);
    }

    public void Activate(int bullets)
    {
        gameObject.SetActive(true);
        this.bullets = bullets;
    }

    public void Deactivate() => gameObject.SetActive(false);

    public void Recharge(int bullets) => this.bullets += bullets;
    #endregion
}
