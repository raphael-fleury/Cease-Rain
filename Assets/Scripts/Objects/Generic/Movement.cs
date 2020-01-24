using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    #region Variables
    protected Rigidbody2D body;

    [Header("Status")]
    public bool onFloor;
    public float knockback;
    [Range(-1,1)]
    public int direction;

    [Header("Options")]
    public float walkSpeed;
    public float jumpForce;
    public float jumpModifier;

    public Feet feet;
    #endregion

    #region Custom Properties
    public virtual bool canMove 
    {
        get { return knockback <= 0; }
    }

    public virtual bool canJump
    {
        get { return canMove && onFloor; }
    }
    #endregion

    protected virtual void Awake() { body = GetComponent<Rigidbody2D>(); }

    protected virtual void FixedUpdate()
    {
        onFloor = feet.OnFloor();

        if (knockback > 0)
            knockback -= 0.02f;
        else if (canMove)
        {
            body.velocity = new Vector2(direction * walkSpeed * (onFloor ? 1 : jumpModifier), body.velocity.y);
            Flip();
        }
    }

    #region Events
    public event Action onFlip;
    protected void Flip()
    {      
        if (direction != 0 && direction != Mathf.Sign(transform.localScale.x))
        {
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;

            if (onFlip != null)
                onFlip();
        }
    }

    protected virtual bool Jump()
    {
        if (!canJump)
        return false;

        body.AddForce(Vector2.up * jumpForce * body.mass, ForceMode2D.Impulse);
        return true;
    }

    public void Knockback(float knockback)
    {
        body.AddForce(Vector2.right * knockback, ForceMode2D.Impulse);
        this.knockback = knockback;
    }
    #endregion
}
