
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    public bool replace = false;
    public string level;

    [Space(10)]
    public GameObject newGameUI;
    public GameObject yesNoButtons;
    public GameObject okButton;

    [Space(10)]
    public InputField input;
    public Text output;

    public string fileAlreadyExists;

    public void OnButtonClick(int mode)
    {
        switch (mode)
        {
            case 0: //New Game Button
                newGameUI.SetActive(true);
                break;
            case 1: //Yes
                replace = true;
                NewGame();
                break;
            case 2: //No
                yesNoButtons.SetActive(false);
                output.text = "";
                break;
        }         
    }

    public void NewGame()
    {
        Debug.Log(Data.FullPath(input.text));
        Debug.Log(Data.FileExists(Data.FullPath(input.text)));
        if (Data.FileExists(Data.FullPath(input.text)) && !replace)
        {
            output.text = fileAlreadyExists;
            yesNoButtons.SetActive(true);
        }
        else
        {
            try
            {
                Game.Save(input.text, SceneEnum.Cutscene);
                Game.LoadScene(SceneEnum.Cutscene);
            }
            catch (System.Exception e)
            {
                output.text = "Error. Invalid name.";
                throw;
            }
           
        }          
    }

    public void CloseMenu() { newGameUI.SetActive(false); }

}
