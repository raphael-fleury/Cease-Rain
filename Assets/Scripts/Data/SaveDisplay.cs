using UnityEngine;
using UnityEngine.UI;

public class SaveDisplay : MonoBehaviour
{
    Button button;

    public FileName file;

    private void Awake()
    {
        button.GetComponentInChildren<Text>().text = "";
    }
}
