using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleEffect : MonoBehaviour
{
    public float amount;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<Marjory>().toxicity += amount;

    }
}
