using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    [SerializeField] GameObject okButton;
    [SerializeField] GameObject yesNoButtons;

    [Space(10)]
    [SerializeField] InputField input;
    [SerializeField] Text output;

    private void OnEnable()
    {
        input.text = "";
        Reset();        
        input.placeholder.gameObject.SetActive(true);
        okButton.GetComponent<Button>().interactable = false;
    }

    public void Reset()
    {
        okButton.GetComponentInChildren<Text>().text = "CREATE";
        okButton.GetComponent<Button>().interactable = true;
        output.text = "";

        yesNoButtons.SetActive(false);
        okButton.SetActive(true);
    }

    public void OnInputChanged(string text)
    {
        Reset();

        SaveName saveName;
        try
        { 
            saveName = new SaveName(text); 
            if(saveName.fileExists)
            {
                output.text = "File already exists.";
                okButton.GetComponentInChildren<Text>().text = "REPLACE";                
            }
        }
        catch (System.Exception e)
        {
            okButton.GetComponent<Button>().interactable = false;
            output.text = e.Message;
        }        
    }

    public void NewGame(bool replace)
    {        
        if (SaveSystem.FileExists(input.text) && !replace)
        {
            output.text = "Are you sure?";
            yesNoButtons.SetActive(true);
            okButton.SetActive(false);
        }
        else
        {
            try
            {
                Game.NewGame(new SaveName(input.text));
            }
            catch (System.Exception e)
            {
                output.text = "Error. " + e.Message;
                throw;
            }         
        }          
    }
}
