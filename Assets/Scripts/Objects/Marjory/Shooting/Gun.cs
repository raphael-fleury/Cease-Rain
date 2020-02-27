using UnityEngine;

public class Gun : MonoBehaviour
{
    protected GameObject shot;

    public Shooter shooter;
    [Header("Status")]
    public int bullets;

    [Header("Options")]
    [SerializeField] float rechargeTime;
    [SerializeField] float rotationFix;

    [Header("References")]
    [SerializeField] AudioSource sound;    
  
    public virtual void Shoot()
    {
        //Debug.Log(transform.rotation + " " + 7.5f * transform.rotation.eulerAngles.z / 360 + " " + vector * speed * 100);
        //Vector2 vector = new Vector2(Mathf.Sign(Level.marjory.transform.localScale.x),  7.5f * transform.rotation.eulerAngles.z / 360);
        Vector2 vector = new Vector2(Mathf.Sign(Level.marjory.transform.localScale.x), Mathf.Abs(transform.rotation.z) * 2 - rotationFix);

        shot = shooter.Shoot(vector * 100);
           
        Level.marjory.recharging = rechargeTime;
        if (bullets < 1)
            Level.marjory.SetGun(Marjory.Guns.Codomoon, int.MaxValue);
        else
            bullets--;
    }

    public void Activate(int bullets)
    {
        gameObject.SetActive(true);
        this.bullets = bullets;
    }

    public void Deactivate() { gameObject.SetActive(false); }

    public void Recharge(int bullets)
    {
        this.bullets += bullets;
    }
}
