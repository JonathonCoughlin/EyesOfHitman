using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(TutorialInput))]
public class TutorialInputPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float widthRatio = 0.5f;

        // Calculate rects
        var keyCodePosition = new Rect(position.x, position.y, position.width * widthRatio, position.height);
        var imagePosition = new Rect(position.x + position.width * widthRatio, position.y, position.width * (1f - widthRatio), position.height);
        
        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(keyCodePosition, property.FindPropertyRelative("m_Control"), GUIContent.none);
        EditorGUI.ObjectField(imagePosition, property.FindPropertyRelative("m_Sprite"), GUIContent.none);
        
        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();

    }

}