﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarjoryMovement : Movement
{
    public enum CurrentMovement { Idle, Running, Jumping, Falling, Knockbacked }
    public CurrentMovement movement;

    public float axisX = 1;

    [Header("References")]
    public List<Animator> animators;

    private void Update()
    {
        if (Input.GetKeyDown(Controls.FindKey("JumpKey"))) { Jump(); }
    }

    protected override void FixedUpdate()
    {       
        base.FixedUpdate();
        axisX = Input.GetAxis("Horizontal");
        if (axisX != 0)
            direction = (int)Mathf.Sign(axisX);

        base.FixedUpdate();
        body.velocity = new Vector2(body.velocity.x * Mathf.Abs(axisX), body.velocity.y);

        Movement();
        animators.ForEach(a => a.SetInteger("movement", (int)movement));      
    }

    void Movement()
    {
        if (!onFloor)
        {
            if (body.velocity.y < 0)
                movement = CurrentMovement.Falling;
            else if (body.velocity.y > 0)
                movement = CurrentMovement.Jumping;
        }
        else
        {
            if (body.velocity.x == 0)
                movement = CurrentMovement.Idle;
            else if (knockback > 0)
                movement = CurrentMovement.Knockbacked;
            else
                movement = CurrentMovement.Running;
        }
        
    }
}
