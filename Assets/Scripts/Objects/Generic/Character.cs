using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector] public float maxLife;
    [SerializeField] float _life;

    protected virtual void Start() { maxLife = life; }

    protected virtual void Death() { }

    public event Action onLifeChange;
    protected virtual void OnLifeChange()
    {
        if (onLifeChange != null)
            onLifeChange();
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
            if (value > maxLife)
                _life = maxLife;
            else if (value < 0)
                _life = 0;
            else
                _life = value;

            OnLifeChange();
        }
    }
}
