using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    #region Fields
    protected Rigidbody2D body;

    [Header("Status")]
    [SerializeField] bool _canMove = true;
    [SerializeField] bool _canFlip = true;
    [SerializeField, Min(0)] float _knockback;

    [Header("Options")]
    [SerializeField, Min(0)] float _walkSpeed;
    #endregion

    #region Events
    private event Action onFlipEvent;
    protected event Action onMoveEvent;

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

    public virtual bool canFlip
    {
        get { return knockback <= 0 && _canFlip; }
        set { _canFlip = value; }
    }

    public int direction
    {
        get { return (int)Mathf.Sign(transform.localScale.x); }
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
    protected virtual void Awake() { body = GetComponent<Rigidbody2D>(); }

    protected virtual void FixedUpdate()
    {
        knockback -= Time.fixedDeltaTime;
        
        if (canMove)
            Move();
    }
    #endregion

    #region Methods
    public bool IsFacing(Transform character)
    {
        return Mathf.Sign(transform.localScale.x) == character.position.x.CompareTo(transform.position.x);
    }

    public void Knockback(float knockback)
    {
        body.AddForce(Vector2.right * knockback, ForceMode2D.Impulse);
        this.knockback = knockback;
    }

    protected virtual void Move()
    {
        body.SetVelocityX(direction * walkSpeed);
        onMoveEvent?.Invoke();
    }

    public bool Flip()
    {
        if (canFlip)
        {
            transform.SetLocalScaleX(-transform.localScale.x);
            onFlipEvent?.Invoke();
        }

        return canFlip;
    }
    #endregion
}
