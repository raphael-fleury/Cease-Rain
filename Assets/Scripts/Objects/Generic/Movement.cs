using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    #region Fields
    protected Rigidbody2D body;

    [Header("Status")]
    [SerializeField] bool _canMove = true;
    [Range(-1,1)] protected int direction;
    [SerializeField] [Min(0)] float _knockback;

    [Header("Options")]
    [SerializeField] float _walkSpeed;
    #endregion

    #region Events
    private event Action onFlipEvent;
    private event Action onMoveEvent;

    public event Action OnFlipEvent
    {
        add { onFlipEvent += value; }
        remove { onFlipEvent -= value; }
    }

    public event Action OnMoveEvent
    {
        add { onMoveEvent += value; }
        remove { onMoveEvent -= value; }
    }
    #endregion

    #region Properties
    public virtual bool canMove 
    {
        get { return knockback <= 0 && _canMove; }
        set { _canMove = value; }
    }

    public float knockback
    {
        get { return _knockback; }
        private set
        {
            if (value < 0)
                _knockback = 0;
            else
                _knockback = value;
        }
    }

    public float walkSpeed
    {
        get { return _walkSpeed; }
    }
    #endregion
  
    #region Unity Methods
    protected virtual void Awake() =>
        body = GetComponent<Rigidbody2D>();

    protected virtual void FixedUpdate()
    {
        knockback -= Time.fixedDeltaTime;
        
        if (canMove)
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
        body.SetVelocityX(direction * walkSpeed);
        onMoveEvent?.Invoke();
    }

    void Flip()
    {   
        if (direction != 0 && direction != Mathf.Sign(transform.localScale.x))
        {
            transform.SetLocalScaleX(-transform.localScale.x);
            onFlipEvent?.Invoke();
        }
    }
    #endregion
}
