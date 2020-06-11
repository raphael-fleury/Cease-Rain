using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Fields
    protected GameObject shot;

    public Shooter shooter;
    [Header("Status")]
    [SerializeField] ushort _bullets;

    [Header("Options")]
    [Min(0)] public float rechargeTime;
    [SerializeField] float rotationFix;

    [Header("References")]
    [SerializeField] AudioSource sound; 
    #endregion  

    public ushort bullets
    {
        get { return _bullets; }
        private set { _bullets = value; }
    }

    #region Methods
    public virtual void Shoot()
    {
        Vector2 vector = new Vector2(Mathf.Sign(Marjory.instance.transform.localScale.x),
                                     Mathf.Abs(transform.rotation.z) * 2 - rotationFix);

        shot = shooter.Shoot(vector);
           
        bullets--;
        if (bullets < 1)
            Marjory.instance.SetGun(Marjory.Guns.Codomoon, ushort.MaxValue);
    }

    public void Activate(ushort bullets)
    {
        gameObject.SetActive(true);
        this.bullets = bullets;
    }

    public void Deactivate() => gameObject.SetActive(false);

    public void Recharge(ushort bullets) => this.bullets += bullets;
    #endregion
}
