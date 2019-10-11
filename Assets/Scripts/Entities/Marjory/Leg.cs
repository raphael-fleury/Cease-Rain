using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    public enum Direction { Back = -1, Front = 1 }

    public float rotateSpeed;
    public float minRotation;
    public float maxRotation;

    public Transform bottomLeg;

    public Movement movement;
    public Direction direction;

    public void Rotate()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * (int)direction);
        bottomLeg.Rotate(Vector3.back * rotateSpeed * (int)direction * 2);

        if (transform.rotation.eulerAngles.z <= minRotation) { direction = Direction.Front; }
        if (transform.rotation.eulerAngles.z >= maxRotation) { direction = Direction.Back; }
    } 
}
