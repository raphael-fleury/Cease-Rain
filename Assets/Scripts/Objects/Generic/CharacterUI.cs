using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    public Movement character;
    
    void Start() => character.OnFlipEvent += Flip;

    void Flip() => transform.localScale *= new Vector2(-1, 1);
}
