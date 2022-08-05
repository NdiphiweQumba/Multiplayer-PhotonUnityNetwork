using UnityEngine;
using UnityEditor;

namespace SimpleTools.Editor
{
    [CustomPropertyDrawer(typeof(AxisList))]
    public class AxisListDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //SimpleEditorHelpers editorHelpers;
            //editorHelpers.DisplayStringList(property, position, StaticEditorHelpers.AxisArray, label);
        }
    }
}