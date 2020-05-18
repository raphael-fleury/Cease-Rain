using UnityEngine;

public class MarjoryDefense : MonoBehaviour
{
    #region Fields
    MarjoryShooting shooting;

    int gun;

    [Header("Status")]
    [SerializeField] bool canDefend;
    [SerializeField] bool _defending;

    [Header("References")]
    [SerializeField] GameObject umbrella;
    [SerializeField] GameObject localUmbrella;
    [SerializeField] string tagUmbrella;
    #endregion

    public bool defending
    {
        get { return _defending; }
        private set
        {
            _defending = value;
            umbrella.SetActive(!value);
            localUmbrella.SetActive(value);
            shooting.SetGun(value ? 1 : gun, 0);
        }
    }

    #region Methods
    void ReleaseUmbrella()
    {
        umbrella.transform.position = localUmbrella.transform.position;
        Debug.Log(umbrella.transform.position);
        defending = false;
    }

    void Awake()
    { 
        shooting = GetComponent<MarjoryShooting>();
    }

    void Update()
    {
        bool wasDefending = defending;
        bool isDefending = Input.GetKey(Controls.FindKey("DefendKey")) && (canDefend || defending);
        isDefending = !Input.GetKeyUp(Controls.FindKey("DefendKey")) && isDefending;

        if (wasDefending != isDefending)
        {
            GetComponent<Animator>().SetBool("defending", isDefending);

            if (isDefending)
            {
                gun = shooting.currentGunIndex;
                defending = true;
            }    
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagUmbrella)
            canDefend = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagUmbrella)
            canDefend = false;
    }
    #endregion
}
