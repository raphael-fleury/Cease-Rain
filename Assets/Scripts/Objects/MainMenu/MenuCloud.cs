using UnityEngine;

public class MenuCloud : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;

    [Space(10)]
    [SerializeField] Transform leftLimit;
    [SerializeField] Transform rightLimit;

    [Space(10)]
    [SerializeField] [Range(0, 255)] int minWhite;    
    [SerializeField] [Range(0, 255)] int minOpacity;

    void Start()
    {
        byte color = (byte)Random.Range(minWhite, 255);        
        GetComponent<SpriteRenderer>().color = new Color32(color, color, color, (byte)Random.Range(minOpacity, 255));
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x - speed / 2000, transform.position.y);
        if (transform.position.x <= leftLimit.position.x)
        {
            transform.position = new Vector3(rightLimit.position.x, transform.position.y);
            speed = Random.Range(minSpeed, maxSpeed);
        }
    }
}
