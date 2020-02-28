using UnityEngine;

public class Enemy : Character
{
    [SerializeField] GameObject deathAnim;

    protected override void Start()
    { 
        base.Start();
        OnDeath += delegate 
        {
            if (deathAnim)
                Instantiate(deathAnim, transform.position, Quaternion.identity);
        };
    }
}
