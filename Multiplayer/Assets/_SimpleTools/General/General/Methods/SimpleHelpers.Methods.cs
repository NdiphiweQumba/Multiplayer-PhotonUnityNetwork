using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SimpleTools
{
    //Extentions
    public static partial class SimpleHelper
    {
        public static bool GlobalLogDisplay;
        public static void LogEvent(string logText, bool displayEvent = true)
        {
            if (GlobalLogDisplay)
            {
                Debug.Log(logText);
                return;
            }

            if (displayEvent) Debug.Log(logText);
        }
            

    }
}