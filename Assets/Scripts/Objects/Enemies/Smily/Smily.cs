using UnityEngine;

[RequireComponent(typeof(Feet), typeof(Animator), typeof(FightMovement))]
public class Smily : Enemy
{
    #region Fields
    Movement movement;
    Animator animator;
    OneJump jump;
    Feet feet;

    [Header("Options")]
    [SerializeField] Shooter shooter;
    [SerializeField, Min(0)] float cooldown = 2f;
    #endregion

    #region Methods
    protected override void Awake()
    {
        base.Awake();
        feet = GetComponent<Feet>();
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        Recharge();
        feet.OnStepEvent += delegate
        { 
            Invoke("Recharge", cooldown);
            movement.canFlip = true;
        };
    }

    void FixedUpdate() { animator.SetBool("idle", !movement.canMove); }

    void Recharge()
    {
        if (feet.onFloor)
        {
            if (!movement.IsFacing(Marjory.instance.transform))
                movement.Flip();

            animator.SetTrigger("recharge");
            movement.canMove = false;
            movement.canFlip = false;
        }            
    }

    void Jump()
    {
        if (jump.TryPerform())
        {
            animator.SetTrigger("jump");
            movement.canMove = true;
        }       
    }

    void Shoot() { shooter.Shoot(Vector2.right * movement.direction * shooter.speed); }
    #endregion
}
