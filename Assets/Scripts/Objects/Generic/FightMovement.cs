using UnityEngine;

public class FightMovement : Movement, ILimits
{
    [SerializeField]
    protected Transform marjory;

    public float minSpace;
    public float minDistance;
    
    public Limits limits;

    public void SetLimits(Limits limits) => this.limits.Set(limits);

    protected bool _canMove;
    public override bool canMove
    {
        get { return _canMove && base.canMove; }
    }

    protected virtual void Start() { marjory = Level.marjory.transform; }

    protected override void FixedUpdate()
    {
        Limits l = limits;
        if (transform.position.x > marjory.position.x)
            l.Set(marjory.position.x + minDistance, limits.higher);
        else
            l.Set(limits.lower, marjory.position.x - minDistance);
        
        if(!l.IsBetween(transform.position.x))
            direction = l.Compare(transform.position.x) * -1;

        _canMove = l.Distance() > minSpace;
      
        if (l.Distance() <= minSpace)
        {
            direction = marjory.position.x.CompareTo(transform.position.x);
            Invoke("Flip", .5f);
        }

        base.FixedUpdate();
    }
}
