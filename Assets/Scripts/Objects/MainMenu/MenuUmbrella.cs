using UnityEngine;

public class MenuUmbrella : MonoBehaviour
{   
    [SerializeField] [Range(-4, 4)] float speed;   
    [SerializeField] [Range(0f,1f)] float maxOffset;

    [Space(10)]
    [SerializeField] float minSpeed = 1;
    [SerializeField] float maxSpeed = 3;

    float height;

    void Start() =>
        height = transform.position.y;

    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed / 2000);
        if (transform.position.y <= height - maxOffset / 10) { speed =  Random.Range(minSpeed, maxSpeed); }
        if (transform.position.y >= height + maxOffset / 10) { speed = -Random.Range(minSpeed, maxSpeed); }
    }
}
