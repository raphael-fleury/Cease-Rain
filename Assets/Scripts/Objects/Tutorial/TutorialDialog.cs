using UnityEngine;
using UnityEngine.UI;

public class TutorialDialog : MonoBehaviour
{
    #region Fields
    [Header("Options")]
    [SerializeField] float writeDelay;

    [Header("References")]
    [SerializeField] Tutorial tutorial;
    [SerializeField] Text textBox;

    float delay;
    string text;   
    int index = 0;
    #endregion

    #region Methods
    public void Write(string text)
    {
        index = 0;
        this.text = text;
        textBox.text = "";
        gameObject.SetActive(true);
        Level.marjory.controllable = false;
    }

    private void Update()
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

    private void End()
    {
        Level.marjory.controllable = true;
        gameObject.SetActive(false);
        tutorial.NextEvent();
    }
    #endregion
}
