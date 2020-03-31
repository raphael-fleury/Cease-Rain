using System;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    protected Marjory player;

    #region Events
    private event Action<Marjory> onPlayerEnterEvent;
    private event Action<Marjory> onPlayerExitEvent;

    public event Action<Marjory> OnPlayerEnterEvent
    {
        add { onPlayerEnterEvent += value; }
        remove { onPlayerEnterEvent -= value; }
    }

    public event Action<Marjory> OnPlayerExitEvent
    {
        add { onPlayerExitEvent += value; }
        remove { onPlayerExitEvent -= value; }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {            
            player = collision.GetComponent<Marjory>();
            onPlayerEnterEvent?.Invoke(player);
            player.interactionIconActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onPlayerExitEvent?.Invoke(collision.GetComponent<Marjory>());
            if (player)
            {
                player.interactionIconActive = false;
                player = null;
            }            
        }
    }
}
