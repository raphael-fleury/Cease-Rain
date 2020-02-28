using UnityEngine;

public class SmilyShot : Shot
{
    [SerializeField] [Min(0)] float knockback;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag(targetTag))
            collision.gameObject.GetComponent<Movement>().Knockback(knockback);
    }
}
