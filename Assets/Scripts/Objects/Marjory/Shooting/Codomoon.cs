using UnityEngine;

public class Codomoon : Gun
{
    [Header("Codomoon")]
    [SerializeField] GameObject fire;

    public override void Shoot()
    {
        base.Shoot();

        if (fire)
        {
            ToggleFire();
            Invoke("ToggleFire", .2f);
        }

    }

    void ToggleFire() { fire.SetActive(!fire.activeSelf); }
}
