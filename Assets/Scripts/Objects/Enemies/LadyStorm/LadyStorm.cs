using UnityEngine;

public class LadyStorm : Enemy
{
    #region Fields
    Movement movement;
    Animator animator;
    Feet feet;
    bool jumping;

    [Header("Options")]
    [SerializeField] float shockOutputY;
    [SerializeField] float jumpCooldown;

    [Header("References")]
    [SerializeField] GameObject shield;
    [SerializeField] GameObject shock;
    #endregion

    #region Unity Methods
    void Awake()
    {        
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        feet = GetComponent<Feet>();
    }

    void Start()
    {
        Invoke("Jump", jumpCooldown);

        GameObject sh = GameObject.Instantiate(shield);
        sh.GetComponent<LadyShield>().lady = transform;

        feet.OnStep += SpawnShock;
    }

    void FixedUpdate()
    {
        animator.SetBool("idle", movement.canMove);
    }
    #endregion

    #region Methods
    void Jump()
    {    
        feet.Jump();
        animator.SetTrigger("jump");
        Invoke("Jump", jumpCooldown);
        jumping = true;
    }

    void SpawnShock()
    {
        if (jumping)
        {
            jumping = false;
            SpawnShock(-1);
            SpawnShock(+1);
        } 
    }

    void SpawnShock(int direction)
    {
        GameObject s = GameObject.Instantiate(shock,
            transform.position + Vector3.up * shockOutputY,
            Quaternion.identity);

        s.GetComponent<Shock>().direction = direction;
    }
    #endregion
}
