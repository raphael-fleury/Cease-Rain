using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marjory : Character
{
    Movement movement;

    [Range(0, 100)]
    public float toxicity;

    public Animator mechArm;
    public Animator normalArm;

    #region Shooting
    [Header("Shooting")]
    public Gun[] guns;
    public enum Guns { None, Umbrella, Codomoon, Footloose, Elvisnator, WordShooter, Crossline }
    public Guns currentGun;
    public float recharging;

    public void SetGun(int gun, int bullets)
    {
        if (guns[(int)currentGun]) //if the current is a gun
        {
            if ((int)currentGun == gun) //if is the same gun
            {
                guns[gun].bullets += bullets;
                if (guns[gun])
                    guns[gun].gameObject.SetActive(true);
                return;
            }
            else
                guns[(int)currentGun].gameObject.SetActive(false);
        }

        if (guns[gun]) //if it's a gun
        {
            guns[gun].gameObject.SetActive(true);
            guns[gun].bullets = bullets;
        }

        currentGun = (Guns)gun;
        mechArm.SetInteger("gun", gun);
        normalArm.SetInteger("gun", gun);
    }

    public void SetGun(Guns gun, int bullets)
    {
        SetGun((int)gun, bullets);
    }

    void UpdateShooting()
    {
        if (Input.GetKey(Controls.FindKey("ShootKey")) && guns[(int)currentGun] && recharging <= 0 && !defending)
            guns[(int)currentGun].Shoot();

        mechArm.SetBool("diagonal", Input.GetKey(Controls.FindKey("DiagonalAimKey")));
    }
    #endregion

    #region Defending
    [Header("Umbrella")]
    public GameObject umbrella;
    public GameObject localUmbrella;
    public string tagUmbrella;
    public bool canDefend;
    public bool defending;

    public void ReleaseUmbrella()
    {
        umbrella.transform.position = localUmbrella.transform.position;
        umbrella.SetActive(true);
        localUmbrella.SetActive(false);
        SetGun(currentGun, 0);
    }

    void UpdateDefense(bool def)
    {
        defending = Input.GetKey(Controls.FindKey("DefendKey")) && (canDefend || defending);
        defending = !Input.GetKeyUp(Controls.FindKey("DefendKey")) && defending;

        if (def != defending)
        {
            GetComponent<Animator>().SetBool("defending", defending);
            mechArm.SetInteger("gun", defending ? 1 : (int)currentGun);
            normalArm.SetInteger("gun", defending ? 1 : (int)currentGun);

            if (defending)
            {
                if (guns[(int)currentGun])
                    guns[(int)currentGun].gameObject.SetActive(false);

                umbrella.SetActive(false);
                localUmbrella.SetActive(true);
            }    
        }
    }
    #endregion

    #region Unity Functions
    void Awake()
    {
        Level.marjory = this;
        movement = GetComponent<Movement>();
    }

    private void FixedUpdate()
    {
        if (toxicity > 0)
            toxicity -= 0.01f;
        if (toxicity > 20)
            life -= 0.02f;
        if (recharging > 0) { recharging -= 0.02f; }
    }

    void Update()
    {
        //HUD.canvas.transform.localScale = movement.Flip();

        UpdateShooting();
        UpdateDefense(defending);

        #if UNITY_EDITOR
        ChangeGun();
        #endif
    }

    #region Trigger
    public void OnTriggerEnter2D(Collider2D collider)
    {
        canDefend = collider.gameObject.tag == tagUmbrella;
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        canDefend = !(collider.gameObject.tag == tagUmbrella);
    }
    #endregion

    #endregion

    protected override void Death() { }

    #region Tests
    private void ChangeGun()
    {
        int aux = (int)KeyCode.Alpha1;
        for (int i = aux; i < aux + 7; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                //Debug.Log((KeyCode)i + " " + (Guns)(i - aux));
                SetGun(i - aux, 60);
            }
        }
    }
    #endregion
}
