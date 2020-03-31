using UnityEngine;

public class ShotElvisnator : Shot
{
    [SerializeField] float rotation;

    protected override void Rotate() =>
        transform.Rotate(Vector3.back * Time.deltaTime * rotation * body.velocity.x);
}
