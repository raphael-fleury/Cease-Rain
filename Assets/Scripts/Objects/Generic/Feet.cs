using System;
using UnityEngine;

public class Feet : MonoBehaviour
{
    #region Fields
    Movement movement;
    Rigidbody2D body;

    bool _onFloor;

    [Header("Status")]
    [SerializeField] bool _canJump = true;

    [Header("Jump")]
    [SerializeField] [Min(0)] float jumpForce;
    [SerializeField] [Range(0,1)] float jumpModifier;

    [Header("Overlap")]
    [SerializeField] OverlapCircle circle;
    [SerializeField] LayerMask floor;
    #endregion

    #region Events
    [HideInInspector] public event Action OnJump;
    [HideInInspector] public event Action OnStep;
    #endregion

    #region Properties
    public bool onFloor
    {
        get 
        {
            return circle.Overlap(floor);  
        }        
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

        if (OnJump != null)
            OnJump();
        return true;
    }

    void Awake()
    { 
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();

        movement.OnMove += delegate
        {
            if (!onFloor)
                body.velocity *= new Vector2(jumpModifier, 1f);           
        };
    }

    void FixedUpdate()
    {
        if (!_onFloor && onFloor && OnStep != null)
            OnStep();

        _onFloor = onFloor;
    }

    //private void Update() { Debug.Log(onFloor); }
    #endregion
}
