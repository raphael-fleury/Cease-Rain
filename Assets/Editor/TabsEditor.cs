using UnityEditor;
using UnityEngine;
using System;

[CustomEditor(typeof(TabbedInspector), true), CanEditMultipleObjects]
public class TabsEditor : Editor
{
    private TabbedInspector myTarget;
    private string[] tabs;
    private SerializedObject soTarget;

    private void OnEnable()
    {
        myTarget = (TabbedInspector)target;
        soTarget = new SerializedObject(target);

        tabs = myTarget.inspectorTabs;
    }

    public override void OnInspectorGUI()
    {
        soTarget.Update();
        EditorGUI.BeginChangeCheck();

        Rect rect = new Rect(0f, 0f, Screen.width, 20);
        myTarget.currentTab = GUI.Toolbar(rect, myTarget.currentTab, tabs);

        Type type = typeof(TabbedInspector);
        foreach (var property in type.GetProperties())
        {
            Debug.Log(property.Name);
            var att = (TabGroupAttribute)Attribute.GetCustomAttribute(property, typeof(TabGroupAttribute));
            if (att != null && att.group == myTarget.inspectorTabs[myTarget.currentTab])
                EditorGUILayout.PropertyField(soTarget.FindProperty(property.Name));
        }

        if (EditorGUI.EndChangeCheck())
        {
            soTarget.ApplyModifiedProperties();
            GUI.FocusControl(null);
        }
    }
}
