using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace SimpleTools.Editor
{
    [CustomPropertyDrawer(typeof(TagList))]
    public class TagListDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            string[] tagArray = InternalEditorUtility.tags;
            int index = tagArray.IndexOf<string>(property.stringValue);

            index = EditorGUI.Popup(position, label.text, index, tagArray);

            property.stringValue = tagArray[index];

            property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }
    }
}