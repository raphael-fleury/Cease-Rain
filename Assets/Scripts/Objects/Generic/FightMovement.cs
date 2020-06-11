using UnityEngine;

public class FightMovement : Movement, ILimits
{
    #region Fields
    Limits actualLimits;

    protected Transform marjory;

    [SerializeField, Min(0)] float minSpace = 5;
    [SerializeField, Min(0)] float minDistance = 7;
    [SerializeField, Min(0)] float maxDistance = 20;

    [SerializeField] Limits limits;
    #endregion

    public override bool canMove
    {
        get { return base.canMove && actualLimits.Distance() > minSpace; }
        set { base.canMove = value; }
    }

    #region Methods
    public void SetLimits(Limits limits) => this.limits.Set(limits);

    protected virtual void Start() { marjory = Marjory.instance.transform; }

    protected override void FixedUpdate()
    {
        if (transform.position.x > marjory.position.x)
            actualLimits.Set(marjory.position.x + minDistance, Mathf.Max(limits.higher, marjory.position.x + maxDistance));
        else
            actualLimits.Set(Mathf.Min(limits.lower, marjory.position.x - maxDistance), marjory.position.x - minDistance);

        if (direction == actualLimits.Compare(transform.position.x))
            Flip();
       
        if (actualLimits.Distance() <= minSpace && !IsFacing(marjory))
            Flip();

        base.FixedUpdate();
    }
    #endregion
}
