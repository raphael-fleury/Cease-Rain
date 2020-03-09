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
                Game.Save(new Save(input.text, SceneEnum.Cutscene));
                Game.LoadScene(SceneEnum.Cutscene);
            }
            catch (System.Exception)
            {
                output.text = "Error. Invalid name.";
                throw;
            }         
        }          
    }
}
