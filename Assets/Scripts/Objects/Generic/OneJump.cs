using UnityEngine;

public class OneJump : Jump
{
    protected Movement movement;
    protected Feet feet;

    public override bool canPerform
    {
        get { return base.canPerform && feet.onFloor && movement.knockback <= 0; }
        set { base.canPerform = value; }
    }

    protected override void Awake()
    {         
        movement = GetComponent<Movement>();
        feet = GetComponent<Feet>();

        movement.OnMoveEvent += delegate
        {
            if (!feet.onFloor)
                body.velocity *= new Vector2(modifier, 1f);           
        };
    }
}
