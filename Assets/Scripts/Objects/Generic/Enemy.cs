using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public Fight fight;

    protected override void Death()
    {
        if (fight)
            fight.EnemyDeath(gameObject);
    }
}
