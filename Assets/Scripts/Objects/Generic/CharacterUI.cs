using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    public Movement character;
    
    void Start() { character.OnFlip += Flip; }

    void Flip()
    {       
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
