using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Fields
    [HideInInspector] public float maxLife;
    [SerializeField] float _life;
    #endregion

    #region Events
    public event Action OnDeath;
    public event Action OnLifeChange;
    #endregion

    #region Properties
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

            if (OnLifeChange != null)
                OnLifeChange();

            if (isDead)
            {
                if (OnDeath != null)
                    OnDeath();

                Death(); 
            }               
        }
    }

    public bool isDead
    { 
        get { return life <= 0; } 
    }
    #endregion

    #region Methods
    protected virtual void Death() { Destroy(gameObject); }

    void Start() { maxLife = life; }
    #endregion
}
