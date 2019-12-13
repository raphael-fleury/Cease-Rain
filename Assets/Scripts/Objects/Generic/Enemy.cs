using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public Image lifeBar;

    new void Update()
    {
        lifeBar.fillAmount = life / maxLife;
    }
}
