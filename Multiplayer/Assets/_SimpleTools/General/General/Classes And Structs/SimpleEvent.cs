using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace SimpleTools.Events
{
    [Serializable]
    public class SimpleEvent : PropertyAttribute
    {
        [SerializeField]
        private GameObject[] targets = new GameObject[0];
        [SerializeField]
        private String[] methodNames = new string[0];
        [SerializeField]
        private MethodInfo[] methodInfoList = new MethodInfo[0];

        public GameObject[] Targets => targets;
        public void Invoke()
        {
        }
    }
}