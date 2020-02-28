using UnityEngine;

public class ShotElvisnator : Shot
{
    [SerializeField] float rotation;

    void Update()
    {
        //Debug.Log(Time.deltaTime + " " + rotation + " " + body.velocity.x);
        transform.Rotate(Vector3.back * Time.deltaTime * rotation * body.velocity.x);
    }
}
