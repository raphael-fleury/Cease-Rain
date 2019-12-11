using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : Shot
{
    public override void Awake()
    {
        base.Awake();
        int num = Random.Range(0, 9);
        GetComponent<Animator>().SetInteger("id", num);
    }
}
