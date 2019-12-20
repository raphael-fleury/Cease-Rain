using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public Fight fight;
    public Image lifeBar;

    protected override void OnLifeChange()
    {
        base.OnLifeChange();
        if (lifeBar)
            lifeBar.fillAmount = life / maxLife;
    }

    protected override void Death()
    {
        if (fight)
            fight.EnemyDeath(this);
    }
}
