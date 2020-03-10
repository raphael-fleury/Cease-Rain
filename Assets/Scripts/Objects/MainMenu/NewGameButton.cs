using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    [SerializeField] GameObject okButton;
    [SerializeField] GameObject yesNoButtons;

    [Space(10)]
    [SerializeField] InputField input;
    [SerializeField] Text output;

    [Space(10)]
    [SerializeField] string fileAlreadyExists;

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
                Game.NewGame(input.text);
            }
            catch (System.Exception e)
            {
                output.text = "Error. " + e.Message;
                throw;
            }         
        }          
    }
}
