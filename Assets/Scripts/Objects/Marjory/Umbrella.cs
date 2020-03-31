using UnityEngine;

public class Umbrella : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform marjory;

    [Header("Options")]
    [SerializeField] [Range(0,1)] float speedMultiplier;
    [SerializeField] float height;

    void FixedUpdate()
    {
        Vector2 distance = transform.position.Distance(marjory.position);
        Vector2 destination = marjory.position + Vector3.up * height;

        float x = Mathf.Abs(marjory.gameObject.GetComponent<Movement>().walkSpeed);
        float y = Mathf.Abs(marjory.gameObject.GetComponent<Rigidbody2D>().velocity.y);

        Vector2 speed = new Vector2(x, y);

        if (speed.x < 0) { speed.x *= -0.1f; }

        transform.position = transform.position.MoveTowards(destination,
            speed * distance * speedMultiplier / 100);
    }
}
