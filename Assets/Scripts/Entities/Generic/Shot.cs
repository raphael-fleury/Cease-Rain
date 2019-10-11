using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    protected Rigidbody2D body;

    [Header("References")]
    public GameObject explosion;
    public string targetTag;

    [Header("Options")]
    public int damage;

    private void Awake() { body = GetComponent<Rigidbody2D>(); }

    protected virtual void Update()
    {
        transform.localScale.Set(Mathf.Sign(body.velocity.x) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (explosion) { Instantiate(explosion, transform.position, Quaternion.identity); }
        if (collision.gameObject.CompareTag(targetTag)) {
            collision.gameObject.GetComponent<Character>().life -= damage;
        }
        Destroy(gameObject);
    }
}
