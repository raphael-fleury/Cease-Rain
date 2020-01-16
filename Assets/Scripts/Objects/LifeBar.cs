using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    Image lifeBar;

    public Character character;
    
    void Awake()
    {
        lifeBar = GetComponent<Image>();
        character.onLifeChange += OnLifeBar;
    }

    void OnLifeBar()
    {
        lifeBar.fillAmount = character.life / character.maxLife;
    }
}
