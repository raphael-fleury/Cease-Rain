using System;

public interface IJump
{
    bool canJump { get; set; }
    event Action OnJumpEvent;
    bool Jump();
}
