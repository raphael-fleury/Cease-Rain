using UnityEngine;

public class ShotWordshooter : Shot
{
    #region Fields
    [Header("Wordshooter")]
    [SerializeField] float explodeTime;
    [SerializeField] Letter letters;

    [HideInInspector]
    public float yOffset;
    #endregion

    #region Methods
    public override void Destroy()
    {
        float direction = Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x);
        for(int i = 0; i < letters.quantity; i++)
        {
            ShootLetter(direction, letters.quantity == 1 ? 0 : (float)i / (letters.quantity - 1) - .5f);
        }

        Destroy(gameObject);
    }

    void ShootLetter(float x, float y)
    {
        //Debug.Log(x + " " + y + " " + (y + (body.velocity.y > 0 ? yOffset : 0)));
        GameObject shot = Instantiate(letters.prefab, transform.position, Quaternion.identity);
        shot.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y + (body.velocity.y > 0 ? yOffset : 0)) * letters.speed * 100);
    }
    #endregion

    #region Structs
    [System.Serializable]
    public struct Letter
    {
        public GameObject prefab;
        public int quantity;
        public float speed;               
    }
    #endregion
}