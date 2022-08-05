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
        //For Lists
        /// <summary>
        /// This extenstion helps you get the index of a specific value in an array
        /// </summary>
        /// <typeparam name="T">
        /// The type of the array
        /// </typeparam>
        /// <param name="array">
        /// The array that will be searched through.
        /// </param>
        /// <param name="value">
        /// The value that will be searched for in the array.
        /// </param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] array, T value)
        {
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(value)) return i;
            }

            return default;
        }


        public static T[] NonRefSelf<T>(this T[] array)
        {
            T[] newArray = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }

            return newArray;
        }

        public static List<T> NonRefSelf<T>(this List<T> list)
        {
            List<T> newList = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                newList.Add(list[i]);
            }

            return newList;
        }

    }
}