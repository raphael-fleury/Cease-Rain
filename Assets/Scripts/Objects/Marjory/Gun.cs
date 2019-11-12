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
    public GameObject fire;

    [Header("Options")]
    public float speed;
    public float rechargeTime;

    [Header("Status")]
    public int bullets;

    public void Shoot()
    {
        //Vector2 vector = new Vector2(Mathf.Sign(Level.marjory.transform.localScale.x),  7.5f * transform.rotation.eulerAngles.z / 360);
        Vector2 vector = new Vector2(Mathf.Sign(Level.marjory.transform.localScale.x), Mathf.Abs(transform.rotation.z) * 2);

        GameObject shot = Instantiate(bullet, output.position, Quaternion.identity);
        shot.GetComponent<Rigidbody2D>().AddForce(vector * speed * 100);
        Debug.Log(transform.rotation + " " + 7.5f * transform.rotation.eulerAngles.z / 360 + " " + vector * speed * 100);
        

        Level.marjory.recharging = rechargeTime;
        if (bullets < 1)
            Level.marjory.SetGun(Marjory.Guns.Codomoon, int.MaxValue);

        if (fire)
        {
            ToggleFire();
            Invoke("ToggleFire", .2f);
        }
    }

    private void ToggleFire() { fire.SetActive(!fire.activeSelf);  }
}
