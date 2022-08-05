using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using SimpleTools.Events;

namespace SimpleTools.Editor
{
    [CustomPropertyDrawer(typeof(SimpleEvent))]
    public class SimpleEventDrawer : PropertyDrawer
    {
        private ReorderableList reorderableList;
        private (
            SerializedObject serializedObject,
            Rect position, 
            GUIContent label, 
            SerializedProperty gameObjectsProperty, 
            (List<SerializedProperty> list, List<int> monoIndexList, List<int> methodIndexList) targetList
            )target;
        private SimpleEvent script;

        private void OnStart(ref Rect position, ref SerializedProperty property, ref GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position.height = EditorGUIUtility.singleLineHeight;
            if (script == null) script = (SimpleEvent)attribute;
            if (target.position == null || target.gameObjectsProperty == null || target.label == null) target = 
                    (
                    property.serializedObject,
                    position, 
                    label, 
                    property.FindPropertyRelative("targets"),
                    (new List<SerializedProperty>(), 
                    new List<int>(),
                    new List<int>()));
            InstantiateReordableList(ref property);
        }
        private void DisplayList(ref Rect position)
        {
            reorderableList.elementHeightCallback = OnHeightCallback;
            reorderableList.drawElementCallback = OnDrawElementCallback;
            reorderableList.onAddCallback = OnAddCallback;
            reorderableList.DoList(position);
        }
        private void AtEnd()
        {
            EditorGUI.EndProperty();
            target.serializedObject.ApplyModifiedProperties();
        }
        #region List Setup
        private void InstantiateReordableList(ref SerializedProperty property)
        {
            if (reorderableList == null)
            {
                reorderableList = new ReorderableList(property.serializedObject, target.gameObjectsProperty, true, true, true, true);
                for (int i = 0; i < target.gameObjectsProperty.arraySize; i++)
                {
                    target.targetList.list.Add(target.gameObjectsProperty.GetArrayElementAtIndex(i));
                    target.targetList.monoIndexList.Add(0);
                    target.targetList.methodIndexList.Add(0);
                };
            }
        }

        private void OnDrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.height = 18f;
            void IncrementHeight(float height = 1.25f)
            {
                rect.y += height * EditorGUIUtility.singleLineHeight;
            }

            EditorGUI.PropertyField(rect, target.targetList.list[index]);
            if (target.targetList.list[index].objectReferenceValue)
            {
                IncrementHeight();
                GameObject gameObject = (GameObject)target.targetList.list[index].objectReferenceValue;
                Component[] components = gameObject.GetComponents<Component>();
                string[] componentNames = new string[components.Length];
                for (int i = 0; i < components.Length; i++)
                {
                    componentNames[i] = components[i].GetType().Name;
                }

                target.targetList.monoIndexList[index] = EditorGUI.Popup(rect, target.targetList.monoIndexList[index], componentNames);
                IncrementHeight();
                var Methods = components[target.targetList.monoIndexList[index]].GetType().GetMethods();
                string[] methodNames = new string[Methods.Length];
                for (int i = 0; i < Methods.Length; i++)
                {
                    methodNames[i] = Methods[i].Name;
                }
                target.targetList.methodIndexList[index] = EditorGUI.Popup(rect, target.targetList.methodIndexList[index], methodNames);

            }
           
            
        }

        private void OnAddCallback(ReorderableList list)
        {
            target.gameObjectsProperty.arraySize++;
            target.targetList.list.Add(target.gameObjectsProperty.GetArrayElementAtIndex(target.gameObjectsProperty.arraySize - 1));
            target.targetList.methodIndexList.Add(0);
            target.targetList.monoIndexList.Add(0);
            
        }

        private float OnHeightCallback(int index)
        {
            return EditorGUIUtility.singleLineHeight * 1.25f * 3;
        }
        #endregion

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            OnStart(ref position, ref property, ref label);
            DisplayList(ref position);
            AtEnd();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 5.5f * EditorGUIUtility.singleLineHeight;
        }
    }
}