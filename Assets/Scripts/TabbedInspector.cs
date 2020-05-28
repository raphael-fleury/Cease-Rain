using UnityEngine;

public class TabbedInspector : MonoBehaviour
{
    [HideInInspector] public string[] inspectorTabs = new string[] { "Status", "References" };
    [HideInInspector] public int currentTab;

    [SerializeField, TabGroup("Status")] int a;
    public bool b;
    [SerializeField] string c;
    [SerializeField] GameObject d;
}
