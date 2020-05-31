using UnityEngine;

public class FightMovement : Movement, ILimits
{
    #region Fields
    Limits actualLimits;

    protected Transform marjory;

    [SerializeField, Min(0)] float minSpace = 5;
    [SerializeField, Min(0)] float minDistance = 7;
    
    [SerializeField] Limits limits;
    #endregion

    public override bool canMove
    {
        get { return base.canMove && actualLimits.Distance() > minSpace; }
        set { base.canMove = value; }
    }

    #region Methods
    public void SetLimits(Limits limits) => this.limits.Set(limits);

    protected virtual void Start() { marjory = Level.marjory.transform; }

    protected override void FixedUpdate()
    {
        if (transform.position.x > marjory.position.x)
            actualLimits.Set(marjory.position.x + minDistance, limits.higher);
        else
            actualLimits.Set(limits.lower, marjory.position.x - minDistance);
        
        if(!transform.position.x.IsBetween(actualLimits))
            direction = actualLimits.Compare(transform.position.x) * -1;

        //canMove = actualLimits.Distance() > minSpace;
      
        if (actualLimits.Distance() <= minSpace && !IsFacing(marjory))
            Flip();

        base.FixedUpdate();
    }
    #endregion
}
