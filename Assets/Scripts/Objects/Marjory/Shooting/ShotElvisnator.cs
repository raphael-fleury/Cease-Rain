using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotElvisnator : Shot
{
    public float rotation;

    public override void Update()
    {
        //Debug.Log(Time.deltaTime + " " + rotation + " " + body.velocity.x);
        transform.Rotate(Vector3.back * Time.deltaTime * rotation * body.velocity.x);
    }
}
