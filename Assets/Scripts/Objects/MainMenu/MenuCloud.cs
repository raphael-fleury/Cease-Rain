using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCloud : MonoBehaviour
{
    public float speed;

    public float minSpeed;
    public float maxSpeed;

    [Space(10)]
    public Transform leftLimit;
    public Transform rightLimit;

    [Space(10)]
    [Range(0, 255)]
    public int minWhite;
    [Range(0, 255)]
    public int minOpacity;

    void Start()
    {
        byte color = (byte)Random.Range(minWhite, 255);        
        GetComponent<SpriteRenderer>().color = new Color32(color, color, color, (byte)Random.Range(minOpacity, 255));
        //Debug.Log(GetComponent<SpriteRenderer>().color);
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
