using UnityEngine;

public class GunBox : MonoBehaviour
{
    #region Fields
    Rigidbody2D body;
    Animator animator;

    [SerializeField] string tagShot;

    [Header("Gun")]
    [SerializeField] Marjory.Guns gun;
    [SerializeField] int amount;

    [Header("Jump")]
    [SerializeField] float force;
    [SerializeField] float delay;
    #endregion

    #region Methods
    public void Disappear()
    {
        Level.marjory.SetGun(gun, amount);
        Destroy(gameObject);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        Jump();
    }

    void Jump()
    {
        animator.SetTrigger("jump");
        body.AddForce(Vector2.up * force * 100);
        Invoke("Jump", delay);   
    }

    void OnCollisionEnter2D(Collision2D element) 
    {
        if(element.gameObject.CompareTag("PlayerShot"))
            animator.SetTrigger("break");
    }
    #endregion
}
