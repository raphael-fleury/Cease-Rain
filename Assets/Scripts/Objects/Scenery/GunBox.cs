using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBox : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;

    public string tagShot;

    [Header("Gun")]
    public Marjory.Guns gun;
    public int amount;

    [Header("Jump")]
    public float force;
    public float delay;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        Jump();
    }

    void Jump()
    {
        animator.SetTrigger("jump");
        body.AddForce(Vector2.up * force * 100);
        Invoke("Jump", delay);   
    }

    public void Disappear() 
    {
        Level.marjory.SetGun(gun, amount);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D element) 
    {
        if(element.gameObject.CompareTag("PlayerShot"))
            animator.SetTrigger("break");
    }
}
