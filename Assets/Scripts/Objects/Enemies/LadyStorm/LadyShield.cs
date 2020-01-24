using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyShield : Character
{
    public Transform lady;

    [Header("Options")]
    public float damage = 5;
    public float knockback = .5f;
    public float regenTime = 5f;

    protected override void Start()
    { 
        base.Start();
        lady.gameObject.GetComponent<Character>().onDeath += Destroy; 
    }

    void Update() { transform.position = lady.position; }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Character>().life -= damage;
            other.gameObject.GetComponent<Movement>().Knockback(knockback);
            Death();
        }
    }

    protected override void Death()
    {
        base.Death();
        life = maxLife;
        Invoke("Regen", regenTime);
        gameObject.SetActive(false);
    }

    void Regen() { gameObject.SetActive(true); }

    void Destroy() { Destroy(gameObject); }
}
