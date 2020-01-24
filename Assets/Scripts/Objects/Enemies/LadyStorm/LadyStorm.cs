using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyStorm : Movement, ILimits
{
    Transform marjory;
    Animator animator;  

    bool jumping;

    #region Inspector
    [Header("Lady Storm")]
    public GameObject shield;
    public GameObject shock;
    public float shockOutputY;

    [Space(10)]
    public float minSpace;
    public float minDistance;
    public float jumpCooldown;
    
    public Limits limits;  
    #endregion

    public void SetLimits(Limits limits) => this.limits.Set(limits);

    bool _canMove;
    public override bool canMove
    {
        get { return _canMove && base.canMove; }
    }

    public override bool canJump
    {
        get { return onFloor; }
    }

    protected override void Awake()
    {        
        base.Awake();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        marjory = Level.marjory.transform;
        Invoke("Jump", jumpCooldown);

        //GameObject sh = GameObject.Instantiate(shield, transform.position, Quaternion.identity);
        GameObject sh = GameObject.Instantiate(shield);
        sh.GetComponent<LadyShield>().lady = transform;
    }

    protected override void FixedUpdate()
    {
        Limits l = limits;
        if (transform.position.x > marjory.position.x)
            l.Set(marjory.position.x + minDistance, limits.higher);
        else
            l.Set(limits.lower, marjory.position.x - minDistance);
        
        if(!l.IsBetween(transform.position.x))
            direction = l.Compare(transform.position.x) * -1;

        _canMove = l.Distance() > minSpace;
        animator.SetBool("idle", l.Distance() <= minSpace);
      
        if (l.Distance() <= minSpace)
        {
            direction = marjory.position.x.CompareTo(transform.position.x);
            Invoke("Flip", .5f);
        }

        base.FixedUpdate();
    }

    protected override bool Jump()
    {       
        animator.SetTrigger("jump");
        Invoke("Jump", jumpCooldown);
        jumping = true;
        return base.Jump(); 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {       
        if (other.gameObject.layer == feet.floorLayer / 32 && jumping)
        {
            jumping = false;
            SpawnShock(-1);
            SpawnShock(+1);
        }
    }

    void SpawnShock(int direction)
    {
        GameObject s = GameObject.Instantiate(shock,
         new Vector3(transform.position.x, transform.position.y + shockOutputY),
         Quaternion.identity);

        s.GetComponent<Shock>().direction = direction;
    }
}
