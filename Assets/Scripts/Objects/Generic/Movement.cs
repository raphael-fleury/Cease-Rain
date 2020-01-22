using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Variables
    protected Rigidbody2D body;

    [Header("Options")]
    public float walkSpeed;
    public float jumpForce;
    public float jumpModifier;

    [Header("Status")]
    public bool onFloor;
    public float knockback;
    [Range(-1,1)]
    public int direction;
    #endregion

    public bool canMove 
    {
        get { return knockback <= 0; }
    }

    #region Unity Functions
    protected virtual void Awake() { body = GetComponent<Rigidbody2D>(); }

    protected virtual void FixedUpdate()
    {
        if (knockback > 0)
            knockback -= 0.02f;
        else
            body.velocity = new Vector2(direction * walkSpeed * (onFloor ? 1 : jumpModifier), body.velocity.y);

        if (canMove)
            Flip();
    }
    #endregion

    #region Events
    public event Action onFlip;
    private void Flip()
    {      
        if (direction != 0 && direction != Mathf.Sign(transform.localScale.x))
        {
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;

            if (onFlip != null)
                onFlip();
        }

        return;
    }

    public void Knockback(float knockback)
    {
        body.AddForce(Vector2.right * knockback, ForceMode2D.Impulse);
        this.knockback = knockback;
    }

    public bool Jump()
    {
        if (knockback > 0)
            return false;

        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        onFloor = false;
        return true;
    }
    #endregion
}
