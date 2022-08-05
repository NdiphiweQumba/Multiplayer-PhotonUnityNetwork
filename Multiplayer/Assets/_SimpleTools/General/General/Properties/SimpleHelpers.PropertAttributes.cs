using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SimpleTools
{
    //Extentions
    public static partial class SimpleHelper { }

    public class TagList : PropertyAttribute { }

    public class AxisList : PropertyAttribute { }

    public class CustomName : PropertyAttribute 
    {
        public string CustomString;

        public CustomName(string customString)
        {
            CustomString = customString;
        }
    }

}