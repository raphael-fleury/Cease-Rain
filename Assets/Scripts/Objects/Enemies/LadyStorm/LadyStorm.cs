using UnityEngine;

public class LadyStorm : FightMovement
{
    #region Properties
    Animator animator;
    Feet feet;
    bool jumping;

    [Header("Lady Storm")]
    public GameObject shield;
    public GameObject shock;
    public float shockOutputY;

    [Space(10)]
    public float jumpCooldown;
    #endregion

    protected override void Awake()
    {        
        base.Awake();
        animator = GetComponent<Animator>();
        feet = GetComponent<Feet>();
    }

    protected override void Start()
    {
        base.Start();
        Invoke("Jump", jumpCooldown);

        GameObject sh = GameObject.Instantiate(shield);
        sh.GetComponent<LadyShield>().lady = transform;

        feet.OnStep += SpawnShock;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        animator.SetBool("idle", !_canMove);
    }

    void Jump()
    {    
        feet.Jump();
        animator.SetTrigger("jump");
        Invoke("Jump", jumpCooldown);
        jumping = true;
    }

    #region Shock
    void SpawnShock()
    {
        Debug.Log("a");
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
