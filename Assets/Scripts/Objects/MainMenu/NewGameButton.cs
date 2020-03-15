using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    public string[] _array = new string[(int)Language.Portuguese + 1];

    [SerializeField] GameObject okButton;
    [SerializeField] GameObject yesNoButtons;

    [Space(10)]
    [SerializeField] InputField input;
    [SerializeField] Text output;

    [Space(10)]
    [SerializeField] string fileAlreadyExists;

    public void OnInputChanged(string text)
    {
        okButton.GetComponent<Button>().interactable = true;
        output.text = "";

        yesNoButtons.SetActive(false);
        okButton.SetActive(true);

        SaveName saveName;
        try
        { 
            saveName = new SaveName(text); 
            if(saveName.fileExists)
            {
                okButton.GetComponent<Button>().interactable = true;
                output.text = "File already exists. Want to override?";
                yesNoButtons.SetActive(true);
                okButton.SetActive(false);
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
            output.text = fileAlreadyExists;
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
