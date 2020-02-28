using UnityEngine;

public class Letter : Shot
{
    void Awake()
    {
        int num = Random.Range(0, 9);
        GetComponent<Animator>().SetInteger("id", num);
    }
}
