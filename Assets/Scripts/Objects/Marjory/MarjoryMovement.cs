using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Feet), typeof(Rigidbody2D))]
public class MarjoryMovement : Movement
{   
    #region Fields
    Feet feet;
    OneJump jump;

    [Header("Marjory")]
    [SerializeField] CurrentMovement movement;

    [Range(-1,1)]
    [SerializeField] float axisX = 1;

    [Header("Animation")]
    [SerializeField] List<Animator> animators;

    public Animator face;
    public Animator mechArm;
    public Animator normalArm;
    #endregion

    public enum CurrentMovement { Idle, Running, Jumping, Falling, Knockbacked }

    #region Methods
    protected override void Move()
    {
        base.Move();
        body.velocity *= new Vector2(Mathf.Abs(axisX), 1f);          
    }

    void UpdateAnimations()
    {
        if (!feet.onFloor)
        {
            if (body.velocity.y < 0)
                movement = CurrentMovement.Falling;
            else if (body.velocity.y > 0)
                movement = CurrentMovement.Jumping;
        }
        else
        {
            if (body.velocity.x == 0)
                movement = CurrentMovement.Idle;
            else if (knockback > 0)
                movement = CurrentMovement.Knockbacked;
            else
                movement = CurrentMovement.Running;
        }  

        animators.ForEach(a => a.SetInteger("movement", (int)movement));
    }

    void Blink()
    {
        Invoke("Blink", Random.Range(.8f, 1.6f));
        face.SetTrigger("blink");
    }
    #endregion

    #region Unity Methods
    protected override void Awake()
    {
        base.Awake();

        feet = GetComponent<Feet>();

        GetComponent<MarjoryShooting>().OnGunChangeEvent += (int value) =>
        {
            mechArm.SetInteger("gun", value);
            normalArm.SetInteger("gun", value);
        };

        Invoke("Blink", 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(Controls.FindKey("JumpKey")))
            jump.TryPerform();

        axisX = Input.GetAxis("Horizontal");

        if (axisX != 0 && direction != Mathf.Sign(axisX))
            Flip();

        UpdateAnimations();
        mechArm.SetBool("diagonal", Input.GetKey(Controls.FindKey("DiagonalAimKey")));
        normalArm.SetBool("diagonal", Input.GetKey(Controls.FindKey("DiagonalAimKey")));
    }
    #endregion
}
