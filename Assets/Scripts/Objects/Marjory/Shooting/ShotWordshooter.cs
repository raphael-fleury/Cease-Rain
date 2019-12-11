using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotWordshooter : Shot
{
    [System.Serializable]
    public struct Letter {
        public GameObject prefab;
        public int quantity;
        public float speed;               
    }

    [Header("Wordshooter")]
    public float explodeTime;
    public Letter letters;

    [HideInInspector]
    public float yOffset;

    // Start is called before the first frame update
    public override void Start() { Invoke("Explode", explodeTime); }

    // Update is called once per frame
    void Explode()
    {
        float direction = Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x);
        for(int i = 0; i < letters.quantity; i++)
        {
            ShootLetter(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x), letters.quantity == 1 ? 0 : (float)i / (letters.quantity - 1) - .5f);
        }
        Destroy();
    }

    void ShootLetter(float x, float y)
    {
        Debug.Log(x + " " + y + " " + (y + (body.velocity.y > 0 ? yOffset : 0)));
        GameObject shot = Instantiate(letters.prefab, transform.position, Quaternion.identity);
        shot.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y + (body.velocity.y > 0 ? yOffset : 0)) * letters.speed * 100);
    }
}
