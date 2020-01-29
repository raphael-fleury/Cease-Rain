using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marjory : Movement
{
    Character character;

    [Range(0, 100)]
    public float toxicity;

    #region Shooting
    public enum Guns { None, Umbrella, Codomoon, Footloose, Elvisnator, WordShooter, Crossline }

    [Header("Shooting")]
    public Guns currentGun;
    public float recharging;

    [Space(10)]
    public Gun[] guns;
    
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

    public void SetGun(Guns gun, int bullets) => SetGun((int)gun, bullets);

    void UpdateShooting()
    {
        if (Input.GetKey(Controls.FindKey("ShootKey")) && guns[(int)currentGun] && recharging <= 0 && !defending)
            guns[(int)currentGun].Shoot();

        mechArm.SetBool("diagonal", Input.GetKey(Controls.FindKey("DiagonalAimKey")));
    }

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

    #region Defending
    [Header("Umbrella")]
    public bool canDefend;
    public bool defending;

    [Space(10)]
    public GameObject umbrella;
    public GameObject localUmbrella;
    public string tagUmbrella;

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

    #region Animation
    [Header("Animation")]
    public List<Animator> animators;
    public Animator mechArm;
    public Animator normalArm;

    void UpdateAnimations()
    {
        Debug.Log(movement);
        if (!onFloor)
        {
            if (body.velocity.y < 0)
                movement = CurrentMovement.Falling;
            else if (body.velocity.y > 0)
                movement = CurrentMovement.Jumping;
        }
        else
        {
            if (body.velocity.x == 0)
                movement = CurrentMovement.Idle;
            else if (knockback > 0)
                movement = CurrentMovement.Knockbacked;
            else
                movement = CurrentMovement.Running;
        }  

        animators.ForEach(a => a.SetInteger("movement", (int)movement));
    }
    #endregion

    #region Movement
    public enum CurrentMovement { Idle, Running, Jumping, Falling, Knockbacked }
    
    [Header("Movement")]
    public CurrentMovement movement;

    [Range(-1,1)]
    public float axisX = 1;

    protected override void Move()
    {
        base.Move();
        body.velocity = new Vector2(body.velocity.x * Mathf.Abs(axisX), body.velocity.y);          
    }
    #endregion

    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();
        Level.marjory = this;
        character = GetComponent<Character>();
    }

    protected override void FixedUpdate()
    {
        if (toxicity > 0)
            toxicity -= 0.01f;
        if (toxicity > 20)
            character.life -= 0.02f;
        if (recharging > 0) { recharging -= 0.02f; }
        
        axisX = Input.GetAxis("Horizontal");
        if (axisX != 0)
            direction = (int)Mathf.Sign(axisX);

        UpdateAnimations();
        base.FixedUpdate();
    }

    void Update()
    {
        if (Input.GetKeyDown(Controls.FindKey("JumpKey"))) { Jump(); }

        UpdateShooting();
        UpdateDefense(defending);

        #if UNITY_EDITOR
        ChangeGun();
        #endif
    }

    #region Triggers and Collisions
    private void OnParticleCollision(GameObject other)
    {
        toxicity += 0.2f;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        canDefend = collider.gameObject.tag == tagUmbrella;
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagUmbrella)
            canDefend = false;
    }
    #endregion

    #endregion
}
