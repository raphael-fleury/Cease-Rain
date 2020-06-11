using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
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

    #region Events
    private event Action<Collision2D> onCollisionEvent;

    public event Action<Collision2D> OnCollisionEvent
    {
        add { onCollisionEvent += value; }
        remove { onCollisionEvent -= value; }
    }
    #endregion

    #region Methods
    protected virtual void Awake() => body = GetComponent<Rigidbody2D>();

    protected virtual void Rotate() => transform.right = body.velocity;

    protected virtual void Flip() =>
        transform.SetLocalScaleX(Mathf.Sign(body.velocity.x) * transform.localScale.x);
    
    protected virtual void Update()
    {
        Rotate();
        //Flip();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        onCollisionEvent?.Invoke(collision);

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
