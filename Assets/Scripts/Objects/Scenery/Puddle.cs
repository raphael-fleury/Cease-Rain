using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    public GameObject effect;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.x != 0)
            effect.SetActive(true);
        else
            effect.SetActive(false);
    }
      
    private void OnTriggerExit2D(Collider2D collision)
    {
        effect.SetActive(false);
    }

}
