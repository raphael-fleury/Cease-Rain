using UnityEngine;

public class MarjoryDefense : MonoBehaviour
{
    #region Fields
    MarjoryShooting shooting;

    int gun;

    [Header("Status")]
    [SerializeField] bool _canDefend;
    [SerializeField] bool _defending;

    [Header("References")]
    [SerializeField] Marjory marjory;
    [SerializeField] Umbrella umbrella;
    [SerializeField] GameObject localUmbrella;
    #endregion

    public bool canDefend
    {
        get { return _canDefend && marjory.umbrellaNear; }
        set { _canDefend = value; }
    }

    public bool defending
    {
        get { return _defending; }
        private set
        {
            _defending = value;
            umbrella.gameObject.SetActive(!value);
            localUmbrella.SetActive(value);
            shooting.SetGun(value ? 1 : gun, 0);
        }
    }

    #region Methods
    void ReleaseUmbrella()
    {
        umbrella.gameObject.transform.position = localUmbrella.transform.position;
        defending = false;
    }

    void Start()
    { 
        shooting = marjory.GetComponent<MarjoryShooting>();
    }

    void Update()
    {
        bool wasDefending = defending;
        bool isDefending = Input.GetKey(Controls.FindKey("DefendKey")) && (canDefend || defending);
        isDefending = !Input.GetKeyUp(Controls.FindKey("DefendKey")) && isDefending;

        if (wasDefending != isDefending)
        {
            if (isDefending)
            {
                gun = shooting.currentGunIndex;
                defending = true;
            } 
            else
            {
                shooting.SetGun(gun, 0);
            }
        }
    }
    #endregion
}
