using UnityEngine;

public class LadyStorm : Enemy
{
    #region Fields
    Movement movement;
    Animator animator;
    Feet feet;

    [Header("Options")]
    [SerializeField] float shockOutputY;
    [SerializeField] float jumpCooldown;

    [Header("References")]
    [SerializeField] GameObject shield;
    [SerializeField] GameObject shock;
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
        InvokeRepeating("Jump", jumpCooldown, jumpCooldown);
        Instantiate(shield).GetComponent<LadyShield>().lady = transform;

        feet.OnStepEvent += delegate
        {
            SpawnShock(-1);
            SpawnShock(+1);
        };
        //feet.OnStepEvent += delegate { Debug.Log("p"); };
    }

    void FixedUpdate() { animator.SetBool("idle", !movement.canMove); }

    void Jump()
    {    
        feet.Jump();
        animator.SetTrigger("jump");
    }

    void SpawnShock(int direction)
    {
        GameObject s = Instantiate(shock,
            transform.position + Vector3.up * shockOutputY,
            Quaternion.identity);

        s.GetComponent<Shock>().direction = direction;
    }
    #endregion
}
