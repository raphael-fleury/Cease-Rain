using UnityEngine;
using UnityEditor;
 
//[CustomPropertyDrawer (typeof (Limits))]
public class LimitsDrawer : PropertyDrawer {
 
    // TODO: this is sort of like, but not exactly like the X positional handling of Vector3
    public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {
 
        float nameWidth = pos.width * .41f;
 
        float labelWidth = 40f;
        float fieldWidth = ((pos.width - nameWidth) / 2f) - labelWidth;
 
        SerializedProperty x = prop.FindPropertyRelative ("lower");
        SerializedProperty y = prop.FindPropertyRelative ("higher");
 
        float posx = pos.x;
 
        int indent = EditorGUI.indentLevel;
 
        EditorGUI.LabelField (new Rect (pos.x, pos.y, nameWidth, pos.height), prop.displayName);
        posx += nameWidth;
 
        // Draw X
        EditorGUI.LabelField (new Rect (posx, pos.y, labelWidth, pos.height), "Lower"); posx += labelWidth;
        EditorGUI.DoubleField (
            new Rect (posx, pos.y, fieldWidth, pos.height), x.doubleValue);  posx += fieldWidth;
 
        // Y
        //EditorGUI.indentLevel = 0;
        EditorGUI.LabelField (new Rect (posx, pos.y, labelWidth, pos.height), "Higher"); posx += labelWidth;
        EditorGUI.DoubleField (
            new Rect (posx, pos.y, fieldWidth, pos.height), y.doubleValue); posx += fieldWidth;
       
        EditorGUI.indentLevel = indent;
    }
}