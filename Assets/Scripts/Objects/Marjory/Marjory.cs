using System.Collections.Generic;
using UnityEngine;

public class Marjory : Movement
{
    Character character;
    Feet feet;

    [Range(0, 100)]
    public float toxicity;

    #region Shooting
    public enum Guns { None, Umbrella, Codomoon, Footloose, Elvisnator, WordShooter, Crossline }
    
    #region Properties  
    [Header("Shooting")]
    [SerializeField] Guns _currentGun;
    public float recharging;

    [Space(10)]
    [SerializeField] Gun[] guns;

    int currentGunIndex
    {
        get { return (int)_currentGun; }
        set 
        {
            _currentGun = (Guns)value;
            mechArm.SetInteger("gun", value);
            normalArm.SetInteger("gun", value);
        }
    }

    Gun currentGun
    { 
        get { return guns[currentGunIndex]; }
    }
    #endregion

    #region Private Functions
    void UpdateShooting()
    {
        if (Input.GetKey(Controls.FindKey("ShootKey")) && currentGun && recharging <= 0 && !defending)
            currentGun.Shoot();

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

    #region Public Functions
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

        currentGunIndex = gun;
    }

    public void SetGun(Guns gun, int bullets) => SetGun((int)gun, bullets);
    #endregion
    #endregion

    #region Defending
    [Header("Umbrella")]
    [SerializeField] bool canDefend;
    [SerializeField] bool defending;

    [Space(10)]
    [SerializeField] GameObject umbrella;
    [SerializeField] GameObject localUmbrella;
    [SerializeField] string tagUmbrella;

    public void ReleaseUmbrella()
    {
        umbrella.transform.position = localUmbrella.transform.position;
        umbrella.SetActive(true);
        localUmbrella.SetActive(false);
        SetGun(_currentGun, 0);
    }

    void UpdateDefense(bool def)
    {
        defending = Input.GetKey(Controls.FindKey("DefendKey")) && (canDefend || defending);
        defending = !Input.GetKeyUp(Controls.FindKey("DefendKey")) && defending;

        if (def != defending)
        {
            GetComponent<Animator>().SetBool("defending", defending);
            mechArm.SetInteger("gun", defending ? 1 : currentGunIndex);
            normalArm.SetInteger("gun", defending ? 1 : currentGunIndex);

            if (defending)
            {
                if (currentGun)
                    currentGun.gameObject.SetActive(false);

                umbrella.SetActive(false);
                localUmbrella.SetActive(true);
            }    
        }
    }
    #endregion

    #region Animation
    [Header("Animation")]
    [SerializeField] List<Animator> animators;
    [SerializeField] Animator mechArm;
    [SerializeField] Animator normalArm;

    void UpdateAnimations()
    {
        if (!feet.onFloor)
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
    [SerializeField] CurrentMovement movement;

    [Range(-1,1)]
    [SerializeField] float axisX = 1;

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
        feet = GetComponent<Feet>();
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
        if (Input.GetKeyDown(Controls.FindKey("JumpKey"))) { feet.Jump(); }

        UpdateShooting();
        UpdateDefense(defending);

        #if UNITY_EDITOR
        ChangeGun();
        #endif
    }

    #region Triggers and Collisions
    void OnParticleCollision(GameObject other)
    {
        toxicity += 0.2f;
    }

    void OnTriggerEnter2D(Collider2D collider)
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
