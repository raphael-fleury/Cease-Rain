using System;
using UnityEngine;

public class Feet : MonoBehaviour
{
    Movement movement;
    Rigidbody2D body;

    bool _onFloor;

    #region Events
    [HideInInspector] public event Action OnJump;
    [HideInInspector] public event Action OnStep;
    #endregion

    [Header("Status")]
    [SerializeField] bool _canJump = true;

    [Header("Jump")]
    [SerializeField] [Min(0)] float jumpForce;
    [SerializeField] [Range(0,1)] float jumpModifier;

    [Header("Overlap")]
    [SerializeField] OverlapCircle circle;
    [SerializeField] LayerMask floor;

    public bool onFloor
    {
        get 
        {
            _onFloor = circle.Overlap(floor);
            return _onFloor;  
        }        
    }

    public bool canJump
    {
        get { return onFloor && _canJump && movement.knockback <= 0; }
        set { _canJump = value; }
    }

    protected virtual void Awake()
    { 
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();

        movement.OnMove += delegate
        {
            if (!onFloor)
            {
                Vector2 velocity = body.velocity;
                velocity.x *= jumpModifier;
                body.velocity = velocity;
            }               
        };

        OnStep += delegate { Debug.Log("b"); };
    }

    void FixedUpdate()
    {
        if (!_onFloor && onFloor && OnStep != null)
            OnStep();
    }

    public bool Jump()
    {
        if (!canJump)
        return false;

        body.AddForce(Vector2.up * jumpForce * body.mass, ForceMode2D.Impulse);

        if (OnJump != null)
            OnJump();
        return true;
    }

    //private void Update() { Debug.Log(onFloor); }
}
