using System;
using UnityEngine;

public class MarjoryShooting : MonoBehaviour
{  
    #region Fields
    [Header("Status")]
    [SerializeField] bool _canShoot = true;
    [SerializeField] Marjory.Guns _currentGun;
    [SerializeField] [Min(0)] float recharging;

    [Header("References")]
    [SerializeField] Gun[] guns;
    #endregion

    #region Events
    private event Action<int> onGunChangeEvent;
    private event Action onShootEvent;

    public event Action<int> OnGunChangeEvent
    {
        add { onGunChangeEvent += value; }
        remove { onGunChangeEvent -= value; }
    }

    public event Action OnShootEvent
    {
        add { onShootEvent += value; }
        remove { onShootEvent -= value; }
    }
    #endregion

    #region Properties
    public bool canShoot
    {
        get { return _canShoot; }
        set { _canShoot = value; }
    }

    public int currentGunIndex
    {
        get { return (int)_currentGun; }
        set { _currentGun = (Marjory.Guns)value; }
    }

    public Gun currentGun
    { 
        get { return guns[currentGunIndex]; }
    }
    #endregion

    #region Methods
    public void SetGun(int gun, int bullets)
    {
        Gun newGun = guns[gun];
        if (currentGun) //if the current is a gun
        {
            if (currentGunIndex == gun) //if it's the same gun
            {
                newGun.Recharge(bullets);
                return;
            }
            else
                currentGun.Deactivate();
        }

        if (newGun) //if it's a gun
            newGun.Activate(bullets);

        onGunChangeEvent?.Invoke(gun);

        currentGunIndex = gun;
    }

    void ChangeGun()
    {
        int aux = (int)KeyCode.Alpha1;
        for (int i = aux; i < aux + 7; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
                SetGun(i - aux, 60);
        }
    }

    bool Shoot()
    {
        if (!currentGun || recharging > 0 || !canShoot)
            return false;

        recharging = currentGun.rechargeTime;
        currentGun.Shoot();

        onShootEvent?.Invoke();

        return true;
    }

    void Update()
    {
        if (Input.GetKey(Controls.FindKey("ShootKey")))
            Shoot();

        if (recharging > 0) { recharging -= Time.deltaTime; }

        #if UNITY_EDITOR
        ChangeGun();
        #endif
    }
    #endregion
}
