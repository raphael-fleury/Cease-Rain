using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUmbrella : MonoBehaviour
{
    [Range(-4, 4)]
    public float speed;
    [Range(0f, 1f)]
    public float maxOffset;

    [Space(10)]
    public float minSpeed = 1;
    public float maxSpeed = 3;

    float height;

    void Start() { height = transform.position.y; }

    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed / 2000);
        if (transform.position.y <= height - maxOffset / 10) { speed =  Random.Range(minSpeed, maxSpeed); }
        if (transform.position.y >= height + maxOffset / 10) { speed = -Random.Range(minSpeed, maxSpeed); }
    }
}
