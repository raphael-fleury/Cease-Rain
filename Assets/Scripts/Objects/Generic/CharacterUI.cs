using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    public Movement movScript;
    
    void Start() { movScript.onFlip += Flip; }

    void Flip()
    {       
        transform.localScale = transform.localScale.ChangeX(movScript.direction * Mathf.Abs(transform.localScale.x));
    }
}
