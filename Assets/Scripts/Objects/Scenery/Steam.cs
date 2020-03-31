using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BoxCollider2D))]
public class Steam : InteractionArea
{
    #region Fields
    [SerializeField] bool active;

    [Header("Options")]
    [SerializeField] float time;
    [SerializeField] float heal;
    [SerializeField] float dry;

    [Header("References")]
    [SerializeField] GameObject steam;
    #endregion

    #region Methods
    private void Enable()
    {
        active = true;
        Invoke("Disable", time);
    }

    private void Disable()
    {
        active = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private void Update()
    {
        if (!player)
            return;

        if (Input.GetKeyDown(Controls.FindKey("InteractionKey")))
        {
            GetComponent<Animator>().SetTrigger("Open");
            //player.transform.SetPositionX(transform.position.x);
        }

        if (active)
        {
            player.life += Time.deltaTime * heal;
            player.toxicity -= Time.deltaTime * dry;
        }
    }
    #endregion
}
