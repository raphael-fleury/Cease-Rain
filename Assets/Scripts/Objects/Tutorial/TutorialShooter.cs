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

    #region Animator
    private void Go()
    {
        tutorial.ActivateArrow(arrowPosition.position, Vector2.down);
        CallShootAnimation();
    }

    private void Shoot()
    {
        Shot shot = shooter.Shoot(Vector2.left).GetComponent<Shot>();
        shot.OnCollision += OnShotCollision;
    }

    private void Close()
    {
        gameObject.SetActive(false);
        tutorial.arrow.gameObject.SetActive(false);
        tutorial.NextEvent();
    }
    #endregion

    private void CallShootAnimation()
    {
        if (playerIn)
            GetComponent<Animator>().SetTrigger("Shoot");
    }      

    private void CallCloseAnimation()
    {
        tutorial.arrow.gameObject.SetActive(false);
        GetComponent<Animator>().SetTrigger("End");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Invoke("CallShootAnimation", cooldown);
            playerIn = true;
            tutorial.arrow.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIn = false;
            tutorial.arrow.gameObject.SetActive(true);
        }           
    }

    private void OnShotCollision(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        
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
