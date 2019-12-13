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

    [Header("Options")]
    public float speed;
    public float rechargeTime;

    [Space(10)]
    public float rotationFix;

    [Header("Status")]
    public int bullets;

    protected GameObject shot;
    public virtual void Shoot()
    {
        //Vector2 vector = new Vector2(Mathf.Sign(Level.marjory.transform.localScale.x),  7.5f * transform.rotation.eulerAngles.z / 360);
        Vector2 vector = new Vector2(Mathf.Sign(Level.marjory.transform.localScale.x), Mathf.Abs(transform.rotation.z) * 2 - rotationFix);

        shot = Instantiate(bullet, output.position, Quaternion.identity);
        shot.GetComponent<Rigidbody2D>().AddForce(vector * speed * 100);
        Debug.Log(transform.rotation + " " + 7.5f * transform.rotation.eulerAngles.z / 360 + " " + vector * speed * 100);
        

        Level.marjory.recharging = rechargeTime;
        if (bullets < 1)
            Level.marjory.SetGun(Marjory.Guns.Codomoon, int.MaxValue);
        else
            bullets--;
    }

}
