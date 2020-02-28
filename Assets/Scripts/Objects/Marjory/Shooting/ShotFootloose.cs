using UnityEngine;

public class ShotFootloose : ShotElvisnator
{ 
    [Header("Footloose")]
    [SerializeField] Collider2D[] colliders;

    protected override void Awake()
    {
        base.Awake();

        int num = Random.Range(0, 100);
        if (num < 35)
            num = 0;
        else if (num < 70)
            num = 1;
        else if (num < 90)
            num = 2;
        else
            num = 3;

        GetComponent<Animator>().SetInteger("id", num);
        colliders[num].enabled = true;
    }
}
