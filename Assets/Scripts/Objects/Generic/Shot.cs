using System;
using UnityEngine;

public class Shot : AutoDestroy
{
    #region Fields
    protected Rigidbody2D body;

    [Header("References")]
    [SerializeField] protected GameObject explosion;
    [SerializeField] protected string targetTag;

    [Header("Options")]
    [SerializeField] protected int damage;
    #endregion

    public event Action<Collision2D> OnCollision;

    #region Methods
    protected virtual void Awake() { body = GetComponent<Rigidbody2D>(); }

    protected virtual void Update()
    {
        transform.localScale.Set(Mathf.Sign(body.velocity.x) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.right = body.velocity;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision?.Invoke(collision);

        if (collision.gameObject.CompareTag(targetTag))
            collision.gameObject.GetComponent<Character>().life -= damage;

        Destroy();
    }

    public override void Destroy()
    {
        if (explosion)
            Instantiate(explosion, transform.position, Quaternion.identity);
    
        Destroy(gameObject);
    }
    #endregion
}
