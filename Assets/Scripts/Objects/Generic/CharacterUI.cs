using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    public Movement character;
    
    void Start() => character.OnFlip += Flip;

    void Flip() => transform.localScale *= new Vector2(-1, 1);
}
