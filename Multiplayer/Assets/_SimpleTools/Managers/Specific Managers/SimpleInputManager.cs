using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SimpleTools.SpecificManagers
{
    [AddComponentMenu("SimpleTools/Specific Managers/Simple Input Manager")]
    public class SimpleInputManager : MonoBehaviour
    {
        [Serializable]
        private struct EventList
        {
            [SerializeField]
            private UnityEvent<float> onInputActive, onInputStillActive, onInputInactive;

            public UnityEvent<float> OnInputActive => onInputActive;
            public UnityEvent<float> OnInputStillActive => onInputStillActive;
            public UnityEvent<float> OnInputInactive => onInputInactive;
        }

        [Serializable]
        private struct AxisInput
        {
            [SerializeField]
            private string axisName;
            [SerializeField]
            private string[] axisList;
            [SerializeField]
            private KeyCode positiveButton, negativeButton;
            [SerializeField]
            private float valueModifier;
            [SerializeField]
            private bool isModified, isRaw, isTrigger, isPedal, isInverted;
            [SerializeField]
            private UnityEvent<float> onAxisValueChanged;

            private float axisValue, buttonValue;
            private bool wasZero;

            public bool DisplayDebugLogs { set; get; }

            public void InputCheck()
            {
                axisValue = 0;
                buttonValue = 0;
                buttonValue += Input.GetKey(positiveButton) ? 1 : 0;
                buttonValue += Input.GetKey(negativeButton) ? -1 : 0;
                if (isTrigger) isRaw = true;
                for (int i = 0; i < axisList.Length; i++)
                {
                    if (Input.GetAxis(axisList[i]) != 0) axisValue = isRaw? Input.GetAxisRaw(axisList[i]) : Input.GetAxis(axisList[i]);
                }


                if (axisValue == 0) axisValue = buttonValue;

                if (isPedal)
                {
                    axisValue = 1 - (axisValue + 1) / 2;
                    if (axisValue == 0.5f) return;
                }

                if (isTrigger && axisValue != 0 && !wasZero) return;
                if (axisValue == 0 && wasZero)
                {
                    return;
                }
                else
                {
                    wasZero = false;
                }

                
                if (axisValue == 0) wasZero = true;
                if (isInverted) axisValue *= -1;
                if (isModified) axisValue *= valueModifier;

                onAxisValueChanged.Invoke(axisValue);
                SimpleHelper.LogEvent($"{axisName} Value: {axisValue}", DisplayDebugLogs);
                
            }
        }

        [Serializable]
        private struct ButtonInput
        {
            [SerializeField]
            private string buttonName;
            [SerializeField]
            private KeyCode[] inputKeys;
            [SerializeField]
            private string[] axisButtonNames;
            [SerializeField]
            private float valueModifier;
            [SerializeField]
            private bool usingValueModifier;
            [SerializeField]
            private EventList eventList;

            private bool stillActive;
            
            public bool DisplayDebugLogs { set; get; }

            public void InputCheck()
            {
                stillActive = false;
                if (inputKeys != null)
                {
                    for (var i = 0; i < inputKeys.Length; i++)
                    {
                        if (Input.GetKeyDown(inputKeys[i]))
                        {
                            eventList.OnInputActive.Invoke(usingValueModifier? valueModifier : 1);
                            SimpleHelper.LogEvent($"{buttonName} pressed", DisplayDebugLogs);
                        }

                        if (Input.GetKey(inputKeys[i]))
                        {
                            stillActive = true;
                        }

                        if (Input.GetKeyUp(inputKeys[i]))
                        {
                            eventList.OnInputInactive.Invoke(0);
                            SimpleHelper.LogEvent($"{buttonName} released", DisplayDebugLogs);
                        }
                    }
                }

                if (axisButtonNames != null)
                {
                    for (var i = 0; i < axisButtonNames.Length; i++)
                    {
                        if (Input.GetButtonDown(axisButtonNames[i]))
                        {
                            eventList.OnInputActive.Invoke(usingValueModifier ? valueModifier : 1);
                            SimpleHelper.LogEvent($"{buttonName} pressed", DisplayDebugLogs);
                        }

                        if (Input.GetButton(axisButtonNames[i]))
                        {
                            stillActive = true;
                        }

                        if (Input.GetButtonUp(axisButtonNames[i]))
                        {
                            eventList.OnInputInactive.Invoke(0);
                            SimpleHelper.LogEvent($"{buttonName} released", DisplayDebugLogs);
                        }
                    }
                }

                if (stillActive)
                {
                    eventList.OnInputStillActive.Invoke(usingValueModifier ? valueModifier : 1);
                    SimpleHelper.LogEvent($"{buttonName} held", DisplayDebugLogs);
                }

            }
        }
        
        [Serializable]
        private struct InputElement
        {
            [SerializeField]
            private AxisInput[] axisInput;
            [SerializeField]
            private ButtonInput[] buttonInput;
            public void InputCheck()
            {
                for (int i = 0; i < buttonInput.Length; i++)
                {
                    buttonInput[i].InputCheck();
                }
                for (int i = 0; i < axisInput.Length; i++)
                {
                    axisInput[i].InputCheck();
                }
            }

            public void DisplayLogs(bool displayLogs)
            {
                for (int i =0; i < buttonInput.Length; i++)
                {
                    buttonInput[i].DisplayDebugLogs = displayLogs;
                }
                for (int i = 0; i < axisInput.Length; i++)
                {
                    axisInput[i].DisplayDebugLogs = displayLogs;
                }
            }
        }

        [SerializeField]
        private bool debug;
        [SerializeField]
        private InputElement inputList;


        private void Update()
        {
            inputList.InputCheck();
            inputList.DisplayLogs(debug);
            
        }

        private void OnGUI()
        {
            if (debug)
            {
                GUILayout.Label("");
            }
        }
        
    }
}