using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected float maxLife;
    public float life;

    protected virtual void Start() { maxLife = life; }

    protected virtual void Death() { }

    protected virtual void Update()
    {
        if (life <= 0) { Death(); }
    }
}
