using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public GameObject bullet;
    public AudioSource sound;
    [Space(10)]
    public Transform output;
    public Transform diagonalOutput;

    [Header("Options")]
    public float speed;
    public float rechargeTime;

    [Header("Status")]
    public int bullets;

    public void Shoot(bool diagonal)
    {
        Vector3 pos = diagonal ? diagonalOutput.position : output.position;
        Vector2 vector = diagonal ? Vector2.one : Vector2.right;

        GameObject shot = Instantiate(bullet, pos, Quaternion.identity);
        shot.GetComponent<Rigidbody2D>().AddForce(vector * speed * Mathf.Sign(transform.localScale.x));
    }
}
