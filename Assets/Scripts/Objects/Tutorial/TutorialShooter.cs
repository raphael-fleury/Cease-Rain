using UnityEngine;

public class TutorialShooter : MonoBehaviour
{
    #region Fields
    [Header("Status")]
    [SerializeField] bool playerIn;

    [Header("Options")]
    [SerializeField] bool needToDefend;
    [SerializeField] float cooldown;

    [Header("References")]
    [SerializeField] Shooter shooter;
    [SerializeField] Tutorial tutorial;
    [SerializeField] Transform arrowPosition;
    #endregion

    #region Methods

    public void Enable(bool needToDefend)
    {
        gameObject.SetActive(true);
        this.needToDefend = needToDefend;
    }

    #region Animator
    private void Go()
    {
        tutorial.ActivateArrow(arrowPosition.position, Vector2.down);
        CallShootAnimation();
    }

    private void Shoot()
    {
        Shot shot = shooter.Shoot(Vector2.left).GetComponent<Shot>();
        shot.OnCollisionEvent += OnShotCollision;
    }

    private void Close()
    {
        gameObject.SetActive(false);
        tutorial.DeactivateArrow();
        tutorial.NextEvent();
    }
    #endregion

    private void CallShootAnimation()
    {
        if (playerIn && !IsInvoking("CallShootAnimation"))
            GetComponent<Animator>().SetTrigger("Shoot");
    }      

    private void CallCloseAnimation()
    {
        tutorial.DeactivateArrow();
        GetComponent<Animator>().SetTrigger("Close");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Invoke("CallShootAnimation", cooldown);
            playerIn = true;
            tutorial.DeactivateArrow();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIn = false;
            tutorial.DeactivateArrow();
        }           
    }

    private void OnShotCollision(Collision2D collision)
    {
        string tag = collision.GetContact(0).collider.gameObject.tag;

        if(tag != "Player")
        {
            if (!needToDefend || tag == "Umbrella")
                CallCloseAnimation();
            else if (playerIn)
                Invoke("CallShootAnimation", cooldown);
        }
        else
            Invoke("CallShootAnimation", cooldown);
    }

    #endregion
}
