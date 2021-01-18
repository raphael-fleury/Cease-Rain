using UnityEngine;

public class Shock : MonoBehaviour 
{
    #region Fields
    [Header("Status")]
    [Range(-1,1)] public int direction = 1;

    [Header("Options")]
    [SerializeField, Min(0)] float damage = 5;
    [SerializeField, Min(0)] float speed;

    [Header("Overlap")]
    [SerializeField] OverlapCircle circle;
    [SerializeField] LayerMask floor;
    #endregion

    #region Methods
	void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + direction * speed, transform.position.y);
        if (!circle.Overlap(floor))
            Destroy(gameObject);
	}

    void OnTriggerEnter2D(Collider2D target) 
    {
        if (target.gameObject.CompareTag("Player"))
        {
            target.gameObject.GetComponent<CharacterLife>().Hurt(damage);
            Destroy(gameObject);
        }
    }
    #endregion
}
