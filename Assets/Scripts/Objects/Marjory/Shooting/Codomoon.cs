using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Codomoon : Gun
{
    [Header("Codomoon")]
    public GameObject fire;

    public override void Shoot()
    {
        base.Shoot();

        if (fire)
        {
            ToggleFire();
            Invoke("ToggleFire", .2f);
        }
    }

    private void ToggleFire() { fire.SetActive(!fire.activeSelf); }
}
