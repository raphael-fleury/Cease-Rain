using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmilyShot : Shot
{
    public float knockback;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag(targetTag)) {
            collision.gameObject.GetComponent<Movement>().Knockback(knockback);
        }
    }
}
