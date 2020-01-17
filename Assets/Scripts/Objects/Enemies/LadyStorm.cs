using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyStorm : Enemy, ILimits
{
    public Limits limits;

    public void SetLimits(Limits limits) => this.limits.Set(limits);

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
