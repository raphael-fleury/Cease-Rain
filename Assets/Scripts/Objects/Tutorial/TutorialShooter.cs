using UnityEngine;

public class TutorialShooter : MonoBehaviour
{
    #region Fields
    [Header("Options")]
    [SerializeField] bool defend;
    [SerializeField] float cooldown;

    [Header("References")]
    [SerializeField] Shooter shooter;
    [SerializeField] Tutorial tutorial;
    [SerializeField] GameObject arrow;

    bool playerIn;
    #endregion

    #region Methods
    public void Activate(bool needToDefend)
    {
        gameObject.SetActive(true);
        defend = needToDefend;
    }

    public void Shoot()
    {
        Shot shot = shooter.Shoot(Vector2.left).GetComponent<Shot>();
        shot.OnCollision += OnShotCollision;
    }

    private void InvokeShot() =>
        GetComponent<Animator>().SetTrigger("Shoot");

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Invoke("InvokeShot", cooldown);
            playerIn = true;
            arrow.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIn = false;
            arrow.SetActive(true);
        }           
    }

    private void OnShotCollision(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        
        if(tag != "Player")
        {
            if (!defend || tag == "Umbrella")
                End();
            else if (playerIn)
                Invoke("InvokeShot", cooldown);
        }
        else
            Invoke("InvokeShot", cooldown);
    }

    private void End()
    {
        tutorial.NextEvent();
        gameObject.SetActive(false);
    }
    #endregion
}
