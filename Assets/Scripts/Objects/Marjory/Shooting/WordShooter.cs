using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordShooter : Gun
{
    public override void Shoot()
    {
        base.Shoot();
        shot.GetComponent<ShotWordshooter>().yOffset = Mathf.Abs(transform.rotation.z) * 2;
    }
}
