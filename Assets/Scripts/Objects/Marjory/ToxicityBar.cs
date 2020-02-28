using UnityEngine;
using UnityEngine.UI;

public class ToxicityBar : MonoBehaviour
{
    Image bar;
    
    [SerializeField] Marjory marjory;

    void Awake() { bar = GetComponent<Image>(); }

    void Update() { bar.fillAmount = marjory.toxicity / 100; }
}
