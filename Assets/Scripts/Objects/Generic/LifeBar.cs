using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Image))]
public class LifeBar : MonoBehaviour
{
    public Character character;
    
    void Awake()
    {
        character.OnLifeChangeEvent += () =>
            GetComponent<Image>().fillAmount = character.life / character.maxLife;        
    }
}
