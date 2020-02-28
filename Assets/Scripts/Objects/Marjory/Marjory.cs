using UnityEngine;

public class Marjory : Character
{
    Character character;
    Feet feet;

    [Range(0, 100)]
    public float toxicity;

    void Awake() { Level.marjory = this; }

    void FixedUpdate()
    {
        if (toxicity > 0)
            toxicity -= Time.fixedDeltaTime / 2;
        if (toxicity > 20)
            character.life -= Time.fixedDeltaTime;
    }

    void OnParticleCollision(GameObject other)
    {
        toxicity += 0.2f;
    }

    protected override void Death() {}
}
