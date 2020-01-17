using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Variables
    protected Rigidbody2D body;

    [Header("Options")]
    public float feetRange;
    public float walkSpeed;
    public float jumpForce;
    public float jumpModifier;

    [Header("Status")]
    public bool onFloor;
    public float knockback;
    public int direction;
    #endregion

    #region Unity Functions
    protected void Awake() { body = GetComponent<Rigidbody2D>(); }

    protected void FixedUpdate()
    {
        if (knockback > 0)
            knockback -= 0.02f;
        else
            body.velocity = new Vector2(direction * walkSpeed * (onFloor ? 1 : jumpModifier), body.velocity.y);

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
        body.AddForce(Vector2.right * knockback);
        this.knockback = knockback;
    }

    protected void Jump()
    {
        if (onFloor)
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    #endregion
}
