using UnityEngine;
using UnityEngine.UI;

public class ToxicityBar : MonoBehaviour
{
    Image bar;
    
    void Awake() { bar = GetComponent<Image>(); }

    void Update() { bar.fillAmount = Level.marjory.toxicity / 100; }
}
