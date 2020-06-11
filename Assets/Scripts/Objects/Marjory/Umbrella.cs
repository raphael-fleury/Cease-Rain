using UnityEngine;

public class Umbrella : MonoBehaviour
{
    Transform marjory;

    [Header("Options")]
    [SerializeField] [Range(0,1)] float speedMultiplier;
    [SerializeField] float height;

    public static Umbrella instance { get; private set; }

    void Awake()
    {
        marjory = Marjory.instance.transform;
        instance = this;
    }

    void FixedUpdate()
    {
        if (!marjory) { return; }

        Vector2 distance = transform.position.Distance(marjory.position);
        Vector2 destination = marjory.position + Vector3.up * height;

        float x = Mathf.Abs(marjory.gameObject.GetComponent<Movement>().walkSpeed);
        float y = Mathf.Abs(marjory.gameObject.GetComponent<Rigidbody2D>().velocity.y);

        Vector2 speed = new Vector2(x, y);

        if (speed.x < 0) { speed.x *= -0.1f; }

        transform.position = transform.position.MoveTowards(destination,
            speed * distance * speedMultiplier / 100);
    }

    void OnDestroy() => instance = null;
}
