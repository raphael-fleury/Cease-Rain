using UnityEngine;

public class SmilyShot : Shot
{
    [SerializeField] [Min(0)] float knockback;
    [SerializeField] [Min(0)] float toxicity;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag(targetTag))
        {
            Debug.Log(collision.gameObject.tag);
            collision.gameObject.GetComponent<Movement>().Knockback(knockback);
            collision.gameObject.GetComponent<Marjory>().toxicity += toxicity;
        }
    }
}
