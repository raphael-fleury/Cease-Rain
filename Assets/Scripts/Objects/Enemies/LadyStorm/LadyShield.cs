using UnityEngine;

public class LadyShield : Character
{
    #region Fields
    public Transform lady;

    [Header("Options")]
    [SerializeField, Min(0)] float damage = 5;
    [SerializeField, Min(0)] float knockback = .5f;
    [SerializeField, Min(0)] float regenTime = 5f;
    #endregion

    #region Methods
    void Start()
    {
        lady.gameObject.GetComponent<Character>().OnDeathEvent += delegate { Destroy(gameObject); };
    }

    void Update() { transform.position = lady.position; }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Character>().life -= damage;
            other.gameObject.GetComponent<Movement>().Knockback(knockback * transform.position.x.CompareTo(other.transform.position.x));
            Death();
        }
    }

    protected override void Death()
    {
        Invoke("Regen", regenTime);
        gameObject.SetActive(false);
    }

    void Regen()
    {
        life = maxLife;
        gameObject.SetActive(true);
    }
    #endregion
}
