using System.Collections.Generic;
using UnityEngine;

public class MarjoryMovement : Movement
{   
    #region Fields
    Feet feet;
    MarjoryShooting shooting;

    [Header("Marjory")]
    [SerializeField] CurrentMovement movement;

    [Range(-1,1)]
    [SerializeField] float axisX = 1;

    [Header("Animation")]
    [SerializeField] List<Animator> animators;
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
    #endregion

    #region Unity Methods
    protected override void Awake()
    {
        base.Awake();

        feet = GetComponent<Feet>();
        shooting = GetComponent<MarjoryShooting>();

        shooting.OnGunChange += (int value) => {
            mechArm.SetInteger("gun", value);
            normalArm.SetInteger("gun", value);
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(Controls.FindKey("JumpKey")))
            feet.Jump();

        axisX = Input.GetAxis("Horizontal");
        if (axisX != 0)
            direction = (int)Mathf.Sign(axisX);

        UpdateAnimations();
        mechArm.SetBool("diagonal", Input.GetKey(Controls.FindKey("DiagonalAimKey")));
    }
    #endregion
}
