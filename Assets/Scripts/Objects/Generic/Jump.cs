using System;
using UnityEngine;

public class Jump : MonoBehaviour, IJump
{
    protected Rigidbody2D body;
    Action onPerformEvent;

    #region Inspector
    [Header("Status")]
    [SerializeField] bool _canPerform = true;

    [Header("Jump")]
    [SerializeField, Min(0)] protected float force;
    [SerializeField, Range(0,1)] protected float modifier;
    #endregion

    #region Properties
    public virtual bool canPerform
    {
        get { return _canPerform; }
        set { _canPerform = value; }
    }

    public event Action OnPerformEvent
    {
        add { onPerformEvent += value; }
        remove { onPerformEvent -= value; }
    }
    #endregion

    #region Methods
    public bool TryPerform()
    {
        if (!canPerform)
            return false;

        Perform();

        onPerformEvent?.Invoke();
        return true;
    }

    protected virtual void Perform()
    {
        body.AddForce(Vector2.up * force * body.mass, ForceMode2D.Impulse);
    }

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    #endregion
}
