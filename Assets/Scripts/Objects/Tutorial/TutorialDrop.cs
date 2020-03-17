using UnityEngine;

public class TutorialDrop : MonoBehaviour
{
    float initialHeight;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        initialHeight = transform.position.y;
    }

    void OnCollisionEnter2D(Collision2D collision) =>
        transform.SetPositionY(initialHeight);
}
