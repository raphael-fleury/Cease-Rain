using System;

public interface IStep
{
    bool onFloor { get; }
    event Action OnStepEvent;
}