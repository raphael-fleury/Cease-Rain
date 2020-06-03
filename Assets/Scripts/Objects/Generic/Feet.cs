using System;
using UnityEngine;

[RequireComponent(typeof(Movement), typeof(Rigidbody2D))]
public class Feet : MonoBehaviour
{
    #region Fields
    Movement movement;
    Rigidbody2D body;
    bool wasOnFloor;

    [Header("Status")]
    [SerializeField] bool _canJump = true;

    [Header("Jump")]
    [SerializeField, Min(0)] float jumpForce;
    [SerializeField, Range(0,1)] float jumpModifier;

    [Header("Overlap")]
    [SerializeField] OverlapCircle circle;
    [SerializeField] LayerMask floor;
    #endregion

    #region Events
    private event Action onJumpEvent;
    private event Action onStepEvent;

    public event Action OnJumpEvent
    {
        add { onJumpEvent += value; }
        remove { onJumpEvent -= value; }
    }

    public event Action OnStepEvent
    {
        add { onStepEvent += value; }
        remove { onStepEvent -= value; }
    }
    #endregion

    #region Properties
    public bool onFloor
    {
        get { return circle.Overlap(floor); }
    }

    public bool canJump
    {
        get { return onFloor && _canJump && movement.knockback <= 0; }
        set { _canJump = value; }
    }
    #endregion

    #region Methods
    public bool Jump()
    {
        if (!canJump)
        return false;

        body.AddForce(Vector2.up * jumpForce * body.mass, ForceMode2D.Impulse);

        onJumpEvent?.Invoke();
        return true;
    }

    void Awake()
    { 
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();

        movement.OnMoveEvent += delegate
        {
            if (!onFloor)
                body.velocity *= new Vector2(jumpModifier, 1f);           
        };

        wasOnFloor = onFloor;
    }

    void FixedUpdate()
    {
        if (!wasOnFloor && onFloor)
            onStepEvent?.Invoke();

        wasOnFloor = onFloor;
    }
    #endregion
}
