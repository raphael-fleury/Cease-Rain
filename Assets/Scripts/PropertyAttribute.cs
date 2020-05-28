using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute { }

public class TabGroupAttribute : PropertyAttribute
{
    public string group { get; private set; }

    public TabGroupAttribute(string group)
    {
        this.group = group;
    }
}
