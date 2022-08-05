using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using SimpleTools;

namespace SimpleTools.Editor
{
    public partial class SimpleEditorHelpers { }

    public static partial class StaticEditorHelper
    {
        private static string[] axisArray;

        public static GUIContent EmptyGUIContent = new GUIContent("");
        public static string[] TagArray => InternalEditorUtility.tags;

        public static string[] AxisArray
        {
            get
            {
                List<string> axisList = new List<string>();
                //Gets and serializes Input Manager File
                Object inputManager = AssetDatabase.LoadAllAssetRepresentationsAtPath("ProjectSettings/InputManager.asset")[0];
                SerializedObject serializedObject = new SerializedObject(inputManager);

                SerializedProperty serializedAxisArray = serializedObject.FindProperty("m_Axes");

                for (var i = 0; i < serializedAxisArray.arraySize; i++)
                {
                    axisList.Add(serializedAxisArray.GetArrayElementAtIndex(i).FindPropertyRelative("m_Name").stringValue);
                }

                axisArray = axisList.ToArray();
                return axisArray;
            }
        }

        public static void DisplayStringListLayout(this SerializedProperty serializedProperty, string[] stringArray, GUIContent label = null, GUILayoutOption style = null)
        {
            int index = stringArray.IndexOf(serializedProperty.stringValue);
            if (index < 0) return;

            index = EditorGUILayout.Popup(label, index, stringArray, style);

            serializedProperty.stringValue = stringArray[index];

            serializedProperty.serializedObject.ApplyModifiedProperties();
        }

    }
}