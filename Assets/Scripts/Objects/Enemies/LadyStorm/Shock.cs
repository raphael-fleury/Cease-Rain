using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : MonoBehaviour {

    [Header("Status")]
    public int direction = 1;
    [Header("Options")]
    public float duration = 2;
    public float damage = 5;
    public float speed;

    [Header("Overlap")]
    public LayerMask floorLayer;
    public float range;
    public Vector2 spherePos;  

	void Start() { Invoke("Destroy", duration); }
    void Destroy() { Destroy(gameObject); }
	
	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x + direction * speed, transform.position.y);
        //Debug.Log(Physics2D.OverlapCircle(transform.position, range, floorLayer));
        if (!Physics2D.OverlapCircle(transform.position, range, floorLayer))
            Destroy(gameObject);
	}

    void OnTriggerEnter2D(Collider2D target) {
        if (target.gameObject.CompareTag("Player")) {
            target.gameObject.GetComponent<Character>().life -= damage;
            Destroy();
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position + spherePos.ToVector3(), range);
    }
}
