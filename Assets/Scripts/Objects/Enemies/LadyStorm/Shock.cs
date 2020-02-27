using UnityEngine;

public class Shock : MonoBehaviour {

    [Header("Status")]
    public int direction = 1;
    [Header("Options")]
    public float duration = 2;
    public float damage = 5;
    public float speed;

    [Header("Overlap")]
    public OverlapCircle circle;
    public LayerMask floor;

	void Start() { Invoke("Destroy", duration); }
	
	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x + direction * speed, transform.position.y);
        if (!circle.Overlap(floor))
            Destroy(gameObject);
	}

    void OnTriggerEnter2D(Collider2D target) {
        if (target.gameObject.CompareTag("Player")) {
            target.gameObject.GetComponent<Character>().life -= damage;
            Destroy(gameObject);
        }
    }
}
