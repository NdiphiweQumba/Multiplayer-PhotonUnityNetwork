using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace SimpleTools.Editor
{
    [CustomPropertyDrawer(typeof(Vector3States))]
    public class Vector3StatesDrawer : PropertyDrawer
    {
        private SerializedProperty x, y, z;
        
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (x == null) x = property.FindPropertyRelative("X"); 
            if (y == null) y = property.FindPropertyRelative("Y"); 
            if (z == null) z = property.FindPropertyRelative("Z"); 
            
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.LabelField(position, label);
            position.x += EditorGUIUtility.labelWidth + 2.5f;
            EditorGUI.LabelField(position, "X");
            position.x += 10;
            EditorGUI.PropertyField(position, x, StaticEditorHelper.EmptyGUIContent);
            position.x += 25;
            EditorGUI.LabelField(position, "Y");
            position.x += 10;
            EditorGUI.PropertyField(position, y, StaticEditorHelper.EmptyGUIContent);
            position.x += 25;
            EditorGUI.LabelField(position, "Z");
            position.x += 10;
            EditorGUI.PropertyField(position, z, StaticEditorHelper.EmptyGUIContent);
            EditorGUI.EndProperty();

            property.serializedObject.ApplyModifiedProperties();
        }
    }
}