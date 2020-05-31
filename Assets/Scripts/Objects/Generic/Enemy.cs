using UnityEngine;

public class Enemy : Character
{
    [SerializeField] GameObject deathAnim;

    protected override void Awake()
    { 
        base.Awake();
        OnDeathEvent += delegate 
        {
            if (deathAnim)
                Instantiate(deathAnim, transform.position, Quaternion.identity);
        };
    }
}
