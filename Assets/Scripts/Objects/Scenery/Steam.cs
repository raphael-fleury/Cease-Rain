using System;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BoxCollider2D))]
public class Steam : InteractionArea
{
    #region Fields
    [SerializeField] bool active;

    [Header("Options")]
    [SerializeField] float duration = 4f;
    [SerializeField] float totalHeal = 40f;
    [SerializeField] float totalDry = 60f;

    [Header("References")]
    [SerializeField] ParticleSystem particles;
    #endregion

    #region Events
    private Action onEnableEvent;
    private Action onDisableEvent;

    public event Action OnEnableEvent
    {
        add { onEnableEvent += value; }
        remove { onEnableEvent -= value; }
    } 

    public event Action OnDisableEvent
    {
        add { onDisableEvent += value; }
        remove { onDisableEvent -= value; }
    }
    #endregion

    #region Methods
    private void Enable()
    {
        active = true;
        particles.Play();
        onEnableEvent?.Invoke();
        Invoke("Disable", duration);
    }

    private void Disable()
    {
        active = false;
        particles.Stop();
        onDisableEvent?.Invoke();
        GetComponent<Collider2D>().enabled = false;

        Marjory.instance.drying = false;
    }

    private void Update()
    {
        if (!player)
            return;

        if (Input.GetKeyDown(Controls.FindKey("InteractionKey")))
        {
            GetComponent<Animator>().SetTrigger("Open");
            player.interactionIconActive = false;
            player.drying = true;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (active)
        {
            player.life += Time.deltaTime * (totalHeal / duration);
            player.toxicity -= Time.deltaTime * (totalDry / duration);
        }
    }
    #endregion
}
