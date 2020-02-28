using UnityEngine;

public class FightMovement : Movement, ILimits
{
    #region Fields
    protected Transform marjory;

    [SerializeField] [Min(0)] float minSpace = 5;
    [SerializeField] [Min(0)] float minDistance = 7;
    
    [SerializeField] Limits limits;
    #endregion

    #region Methods
    public void SetLimits(Limits limits) => this.limits.Set(limits);

    protected virtual void Start() { marjory = Level.marjory.transform; }

    protected override void FixedUpdate()
    {
        Limits l = limits;
        if (transform.position.x > marjory.position.x)
            l.Set(marjory.position.x + minDistance, limits.higher);
        else
            l.Set(limits.lower, marjory.position.x - minDistance);
        
        if(!l.IsBetween(transform.position.x)) //check if the enemy is between the limits
            direction = l.Compare(transform.position.x) * -1;

        _canMove = l.Distance() > minSpace;
      
        if (l.Distance() <= minSpace)
        {
            direction = marjory.position.x.CompareTo(transform.position.x);
            Invoke("Flip", .5f);
        }

        base.FixedUpdate();
    }
    #endregion
}
