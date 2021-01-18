using System;
using UnityEngine;

[RequireComponent(typeof(Movement), typeof(Rigidbody2D))]
public class Feet : MonoBehaviour, IStep
{
    #region Fields
    bool wasOnFloor;

    [Header("Overlap")]
    [SerializeField] OverlapCircle circle;
    [SerializeField] LayerMask floor;
    #endregion

    #region Events
    private event Action onStepEvent;

    public event Action OnStepEvent
    {
        add { onStepEvent += value; }
        remove { onStepEvent -= value; }
    }
    #endregion

    #region Properties
    public bool onFloor => circle.Overlap(floor);
    #endregion

    #region Methods
    void Awake() => wasOnFloor = onFloor;

    void FixedUpdate()
    {
        if (!wasOnFloor && onFloor)
            onStepEvent?.Invoke();

        wasOnFloor = onFloor;
    }
    #endregion
}
