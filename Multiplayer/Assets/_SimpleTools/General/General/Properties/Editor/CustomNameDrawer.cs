using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SimpleTools
{
    [CustomPropertyDrawer(typeof(CustomName))]
    public class CustomNameDrawer : PropertyDrawer
    {
        private CustomName customName;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            customName = (CustomName)attribute;
            EditorGUI.PropertyField(position, property, new GUIContent(customName.CustomString));
            EditorGUI.EndProperty();
        }
    }
}