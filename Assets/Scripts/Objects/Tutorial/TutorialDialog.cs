using UnityEngine;
using UnityEngine.UI;

public class TutorialDialog : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] float writeDelay;

    [Header("References")]
    [SerializeField] Tutorial tutorial;
    [SerializeField] Text textBox;

    float delay;
    string text;   
    int index = 0;
    
    public void Write(string text)
    {
        index = 0;
        this.text = text;
        textBox.text = "";       
        gameObject.SetActive(true);
    }

    void End()
    {
        gameObject.SetActive(false);
        tutorial.NextEvent();             
    }

    void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }

        if (index < text.Length)
        {
            textBox.text += text[index];
            delay = writeDelay;
            index++;
        }

        if (Input.GetKeyDown(Controls.FindKey("InteractionKey")))
        {
            End();
            return;
        }
    }
}
