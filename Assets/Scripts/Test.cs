using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed;

    Vector3 initalPos;
    public event Action<Vector2> OnMoveEvent;

    private void Start()
    {
        initalPos = transform.position;
        OnMoveEvent += Log;
        OnMoveEvent += (a) => { initalPos = transform.position; };
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        OnMoveEvent?.Invoke(transform.position - initalPos);
    }

    private void Log(Vector2 a)
    {
        Debug.Log(a);
    }
}
