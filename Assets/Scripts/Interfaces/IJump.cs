using System;

public interface IJump
{
    bool canPerform { get; set; }
    event Action OnPerformEvent;
    bool TryPerform();
}
