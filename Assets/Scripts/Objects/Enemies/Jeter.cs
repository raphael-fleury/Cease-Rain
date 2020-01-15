using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jeter : Enemy
{
    [Header("Status")]
    public int direction;

    [Header("Options")]
    public float speed;
    public Limits limits;

    protected override void Start()
    {
        base.Start();
        if (fight)
        {
            limits.lower = fight.limits.lower;
            limits.higher = fight.limits.higher;
        }
    }

    void Update()
    {
        if(!limits.IsBetween(transform.position.x))
            direction = limits.Compare(transform.position.x) * -1;

        transform.position = transform.position.ChangeX(transform.position.x + direction * (speed / 100));
    }

    protected override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }
}
