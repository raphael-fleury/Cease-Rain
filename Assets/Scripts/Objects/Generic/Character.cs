using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float _life;

    #region Events
    private event Action onDeath;
    private event Action onLifeChange;

    public event Action OnDeath
    {
        add { onDeath += value; }
        remove { onDeath -= value; }
    }

    public event Action OnLifeChange
    {
        add { onLifeChange += value; }
        remove { onLifeChange -= value; }
    }
    #endregion

    #region Properties
    public float maxLife { get; private set; }

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

            onLifeChange?.Invoke();

            if (life <= 0)
            {
                onDeath?.Invoke();
                Death(); 
            }               
        }
    }
    #endregion

    #region Methods
    protected virtual void Death() { Destroy(gameObject); }

    protected virtual void Start() { maxLife = life; }
    #endregion
}
