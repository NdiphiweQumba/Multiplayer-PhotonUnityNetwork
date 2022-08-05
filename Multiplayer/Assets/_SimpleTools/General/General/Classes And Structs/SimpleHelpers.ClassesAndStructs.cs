using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SimpleTools
{
    //Classes and Structs
    [Serializable]
    public static partial class SimpleHelper { }

    [Serializable]
    public class UnityEventElement
    {
        public string EventName;
        public UnityEvent Event;
    }

    [Serializable]
    public class Vector3States
    {
        public bool X, Y, Z;
        public void SetAll(bool state)
        {
            X = state;
            Y = state;
            Z = state;
        }
    }
}