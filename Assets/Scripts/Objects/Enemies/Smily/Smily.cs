﻿using UnityEngine;

public class Smily : FightMovement
{
    Animator anim;
    Feet feet;
    
    [Header("Smily")]
    [SerializeField] CurrentAction _currentAction;

    [Space(10)] 
    [SerializeField] [Min(0)] float jumpCooldown = 3f;

    [SerializeField] Shooter shooter;
    [SerializeField] float knockbackOnShoot;

    enum CurrentAction { Idle, Walking, Recharging, Jumping, Falling }

    CurrentAction currentAction
    {
        get { return _currentAction; }
        set 
        {
            _currentAction = value;
            anim.SetInteger("action", (int)value);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        feet.OnStep += delegate
        {
            currentAction = CurrentAction.Walking;
            if (!IsInvoking("Jump"))
                Invoke("Jump", jumpCooldown);          
        };
    }

    public void Shoot()
    {
        shooter.Shoot(Vector2.right * direction);
        body.AddForce(Vector2.right * direction * knockbackOnShoot);
    }
}
