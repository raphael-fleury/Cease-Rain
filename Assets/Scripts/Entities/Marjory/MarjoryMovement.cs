using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarjoryMovement : Movement
{
    public enum CurrentMovement { Idle, Running, Jumping, Falling, Knockbacked }
    public CurrentMovement movement;

    [Header("Parts")]
    public List<Animator> animators;

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        GetMovement();

        switch (movement)
        {
            case CurrentMovement.Idle:
                break;
            case CurrentMovement.Running:
                break;
            case CurrentMovement.Jumping:
                break;
            case CurrentMovement.Falling:
                break;
            case CurrentMovement.Knockbacked:
                break;
            default:
                break;
        }

        animators.ForEach(a => a.SetInteger("movement", (int)movement));
    }

    void GetMovement()
    {
        if (body.velocity.y < 0)
            movement = CurrentMovement.Falling;
        else if (body.velocity.y > 0)
            movement = CurrentMovement.Jumping;
        else if (body.velocity.x == 0)
            movement = CurrentMovement.Idle;
        else if (knockback > 0)
            movement = CurrentMovement.Knockbacked;
        else
            movement = CurrentMovement.Running;

        //Debug.Log(movement);
    }
}
