using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyShield : Character
{
    [Header("Options")]
    public float damage = 5;
    public float knockback = .5f;
    public float regenTime = 5f;

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
}
