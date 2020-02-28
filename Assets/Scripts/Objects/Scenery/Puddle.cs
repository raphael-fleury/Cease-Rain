using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> particles;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D body = collision.gameObject.GetComponent<Rigidbody2D>();
        if (body)
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.x != 0)
                particles.ForEach(p => p.Emit((int)Mathf.Round(Mathf.Abs(body.velocity.x))));
        }
        
    }

}
