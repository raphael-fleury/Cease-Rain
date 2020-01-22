using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyStorm : Enemy, ILimits
{
    Transform marjory;
    Animator animator;  
    Movement movement;
    Rigidbody2D body;

    [Header("Options")]
    public float minDistance;
    public float jumpCooldown;
    public float shockOutputY;

    public Limits limits;

    [Header("References")]
    public LayerMask floor;
    public GameObject shock;

    public void SetLimits(Limits limits) => this.limits.Set(limits);

    void Awake()
    {        
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        marjory = Level.marjory.transform;
        Invoke("Jump", jumpCooldown);
        movement.onFloor = true;
    }

    void FixedUpdate()
    {
        Limits l = limits;
        if (transform.position.x > marjory.position.x)
            l.Set(marjory.position.x + minDistance, limits.higher);
        else
            l.Set(limits.lower, marjory.position.x - minDistance);
        
        if(!l.IsBetween(transform.position.x))
            movement.direction = l.Compare(transform.position.x) * -1;

        //if (movement.onFloor)
        //    body.velocity = body.velocity.ChangeX(movement.direction * movement.walkSpeed);
    }

    void Jump()
    {
        animator.SetTrigger("jump");
        Invoke("Jump", jumpCooldown);       
        movement.Jump();     
    }

    private void OnCollisionEnter2D(Collision2D other)
    {       
        if (other.gameObject.layer == floor / 32)
        {
            movement.onFloor = true;
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
