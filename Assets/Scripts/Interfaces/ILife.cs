using System;

public interface ILife
{
    float maxAmount { get; }
    float amount { get; }

    event Action OnDeathEvent;
    event Action OnChangeEvent;

    void Hurt(float amount);
    void Heal(float amount);
}
