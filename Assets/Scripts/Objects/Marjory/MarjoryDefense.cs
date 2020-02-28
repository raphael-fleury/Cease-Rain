using UnityEngine;

public class MarjoryDefense : MonoBehaviour
{
    #region Fields
    MarjoryShooting shooting;
    MarjoryMovement movement;

    [Header("Status")]
    [SerializeField] bool canDefend;
    [SerializeField] bool defending;

    [Header("References")]
    [SerializeField] GameObject umbrella;
    [SerializeField] GameObject localUmbrella;
    [SerializeField] string tagUmbrella;
    #endregion

    #region Methods
    public void ReleaseUmbrella()
    {
        umbrella.transform.position = localUmbrella.transform.position;
        umbrella.SetActive(true);
        localUmbrella.SetActive(false);
        shooting.SetGun(shooting.currentGunIndex, 0);
    }
    #endregion

    #region Unity Methods
    void Awake() { shooting = GetComponent<MarjoryShooting>(); }

    void Update()
    {
        bool wasDefending = defending;
        defending = Input.GetKey(Controls.FindKey("DefendKey")) && (canDefend || defending);
        defending = !Input.GetKeyUp(Controls.FindKey("DefendKey")) && defending;

        if (wasDefending != defending)
        {
            GetComponent<Animator>().SetBool("defending", defending);
            movement.mechArm.SetInteger("gun", defending ? 1 : shooting.currentGunIndex);
            movement.normalArm.SetInteger("gun", defending ? 1 : shooting.currentGunIndex);

            if (defending)
            {
                if (shooting.currentGun)
                    shooting.currentGun.gameObject.SetActive(false);

                umbrella.SetActive(false);
                localUmbrella.SetActive(true);
            }    
        }
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
}
