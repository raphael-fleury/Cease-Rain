using UnityEngine;

public class Umbrella : MonoBehaviour {

    [Header("References")]
    public Transform marjory;

    [Header("Options")]
    public float pctVel;
    public float height;

	void FixedUpdate () {

        Vector2 distance;
        Vector2 speed;
        Vector2 destination;

        float x, y;

        #region Speed:
        x = Mathf.Abs(marjory.gameObject.GetComponent<Movement>().walkSpeed);
        y = Mathf.Abs(marjory.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        speed = new Vector2(x, y);

        if (speed.x < 0) { speed.x *= -0.1f; }
        #endregion

        #region  Distance: 
        x = Mathf.Abs(marjory.position.x - transform.position.x);
        y = Mathf.Abs(marjory.position.y - transform.position.y);
        distance = new Vector2(x, y);
        #endregion

        destination = marjory.position + Vector3.up * height;

        x = Mathf.MoveTowards(transform.position.x, destination.x, speed.x * distance.x * pctVel / 10000);
        y = Mathf.MoveTowards(transform.position.y, destination.y, speed.y * distance.y * pctVel / 10000);

        transform.position = new Vector2(x, y);
	}

}
