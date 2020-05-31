using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float _life;

    #region Events
    private event Action onDeathEvent;
    private event Action onLifeChangeEvent;

    public event Action OnDeathEvent
    {
        add { onDeathEvent += value; }
        remove { onDeathEvent -= value; }
    }

    public event Action OnLifeChangeEvent
    {
        add { onLifeChangeEvent += value; }
        remove { onLifeChangeEvent -= value; }
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

            onLifeChangeEvent?.Invoke();

            if (life <= 0)
            {
                onDeathEvent?.Invoke();
                Death(); 
            }               
        }
    }
    #endregion

    #region Methods
    protected virtual void Death() { Destroy(gameObject); }

    protected virtual void Awake() { maxLife = life; }
    #endregion
}
