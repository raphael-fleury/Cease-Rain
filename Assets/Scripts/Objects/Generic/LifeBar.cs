using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LifeBar : MonoBehaviour
{
    public CharacterLife life;
    
    void Awake()
    {
        life.OnChangeEvent += () =>
            GetComponent<Image>().fillAmount = life.amount / life.maxAmount;        
    }
}
