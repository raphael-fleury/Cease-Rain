using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public GameObject deathAnim;

    protected override void Death()
    {
        base.Death();
        if (deathAnim)
            Instantiate(deathAnim, transform.position, Quaternion.identity);
    }
}
