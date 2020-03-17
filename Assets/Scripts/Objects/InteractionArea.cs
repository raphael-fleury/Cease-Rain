using System;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    public bool isPlayerIn;

    //public event Action OnPlayerEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerIn = false;
        }
    }
}
