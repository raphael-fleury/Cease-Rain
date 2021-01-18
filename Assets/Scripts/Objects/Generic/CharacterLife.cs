using System;
using UnityEngine;

public class CharacterLife : MonoBehaviour, ILife
{
    [SerializeField] float _life;

    #region Events
    private event Action onDeathEvent;
    private event Action onChangeEvent;

    public event Action OnDeathEvent
    {
        add { onDeathEvent += value; }
        remove { onDeathEvent -= value; }
    }

    public event Action OnChangeEvent
    {
        add { onChangeEvent += value; }
        remove { onChangeEvent -= value; }
    }
    #endregion

    #region Properties
    public float maxAmount { get; private set; }

    public float amount
    {
        get { return _life; }
        set
        {
            if (value > maxAmount)
                _life = maxAmount;
            else if (value < 0)
                _life = 0;
            else
                _life = value;

            onChangeEvent?.Invoke();

            if (amount <= 0)
            {
                onDeathEvent?.Invoke();
                Death(); 
            }               
        }
    }
    #endregion

    #region Methods
    protected virtual void Death() { Destroy(gameObject); }

    protected virtual void Awake() { maxAmount = amount; }

    public void Hurt(float amount) => this.amount -= amount;
    public void Heal(float amount) => this.amount += amount;
    #endregion
}
