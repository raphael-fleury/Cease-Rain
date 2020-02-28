using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Fields
    protected Rigidbody2D body;

    [Header("Status")]
    [SerializeField] protected bool _canMove = true;
    [Range(-1,1)] public int direction;
    [Min(0)] public float knockback;

    [Header("Options")]
    public float walkSpeed;
    #endregion

    #region Events
    public event Action OnFlip;
    public event Action OnMove;
    #endregion

    #region Properties
    public virtual bool canMove 
    {
        get { return knockback <= 0 && _canMove; }
        set { _canMove = value; }
    }
    #endregion
  
    #region Unity Methods
    protected virtual void Awake() { body = GetComponent<Rigidbody2D>(); }

    protected virtual void FixedUpdate()
    {
        if (knockback > 0)
            knockback -= Time.fixedDeltaTime;
        else if (canMove)
            Move();
    }
    #endregion

    #region Methods
    public void Knockback(float knockback)
    {
        body.AddForce(Vector2.right * knockback, ForceMode2D.Impulse);
        this.knockback = knockback;
    }

    protected virtual void Move()
    {
        Flip();
        body.velocity = new Vector2(direction * walkSpeed, body.velocity.y);

        if (OnMove != null)
            OnMove();
    }

    void Flip()
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
    #endregion
}
