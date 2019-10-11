using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotElvisnator : Shot
{
    [Header("Elvisnator")]
    public float rotation;

    void Update()
    {
        transform.Rotate(Vector3.back * Time.deltaTime * rotation * body.velocity.x);
    }
}
