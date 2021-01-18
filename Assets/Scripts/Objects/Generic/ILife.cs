using System;

public interface ILife
{
    float maxLife { get; }
    float life { get; }

    event Action OnDeathEvent;
    event Action OnLifeChangeEvent;

    void Hurt(float amount);
    void Heal(float amount);
}
