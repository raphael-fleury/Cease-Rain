using UnityEngine;

public class Letter : Shot
{
    protected override void Awake()
    {
        base.Awake();
        int num = Random.Range(0, 9);
        GetComponent<Animator>().SetInteger("id", num);
    }
}
