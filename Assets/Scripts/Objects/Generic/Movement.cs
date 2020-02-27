using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Properties
    protected Rigidbody2D body;

    [Header("Status")]
    public float knockback;
    [Range(-1,1)]
    public int direction;

    [Header("Options")]
    public float walkSpeed;   

    public virtual bool canMove 
    {
        get { return knockback <= 0; }
    }
    #endregion

    public event Action OnFlip;
    public event Action OnMove;

    protected virtual void Awake()
    { 
        body = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        if (knockback > 0)
            knockback -= Time.fixedDeltaTime;
        else if (canMove)
            Move();
    }

    protected virtual void Move()
    {
        Flip();
        body.velocity = new Vector2(direction * walkSpeed, body.velocity.y);

        if (OnMove != null)
            OnMove();
    }

    protected void Flip()
    {      
        if (direction != 0 && direction != Mathf.Sign(transform.localScale.x))
        {
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;

            if (OnFlip != null)
                OnFlip();
        }
    }



    public void Knockback(float knockback)
    {
        body.AddForce(Vector2.right * knockback, ForceMode2D.Impulse);
        this.knockback = knockback;
    }
}
