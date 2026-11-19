using UnityEngine;

public class SingleJump : Jump
{
    protected Movement movement;
    protected Feet _feet;

    public override bool canPerform
    {
        get { return base.canPerform && feet.onFloor && movement.knockback <= 0; }
        set { base.canPerform = value; }
    }

    public Feet feet => _feet;

    protected override void Awake()
    {         
        base.Awake();
        movement = GetComponent<Movement>();
        _feet = GetComponent<Feet>();

        movement.OnMoveEvent += delegate
        {
            if (!feet.onFloor)
                body.velocity *= new Vector2(modifier, 1f);           
        };
    }
}
