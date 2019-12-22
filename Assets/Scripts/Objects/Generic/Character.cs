using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected float maxLife;
    [SerializeField]
    float _life;

    protected virtual void Start() { maxLife = life; }

    protected virtual void Death() { }

    protected virtual void OnLifeChange()
    {
        if (isDead)
            Death();
    }

    public bool isDead
    { 
        get { return life <= 0; } 
    }

    public float life
    {
        get { return _life; }
        set
        { 
            _life = value;
            OnLifeChange();
        }
    }
}
