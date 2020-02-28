using UnityEngine;

public class Enemy : Character
{
    [SerializeField] GameObject deathAnim;

    void Start()
    { 
        OnDeath += delegate 
        {
            if (deathAnim)
                Instantiate(deathAnim, transform.position, Quaternion.identity);
        };
    }
}
