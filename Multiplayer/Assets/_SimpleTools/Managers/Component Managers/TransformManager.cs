using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleTools;

namespace SimpleTools.ComponentManagers
{
    [DisallowMultipleComponent, AddComponentMenu("SimpleTools/Component Managers/Transform Manager")]
    public class TransformManager : MonoBehaviour
    {
        private Vector3 translateByValue, rotateByValue, rotatingValue;
        private Space translateSpace, rotateSpace;

        [SerializeField]
        private bool debug;
        [SerializeField]
        private float translateBySpeed = 1, rotateBySpeed = 90, rotatingModifier = 360;

        public float TranslateBySpeed => translateBySpeed;

        #region Translate Methods
        public void ModifyTranslateSpeedBy(float value)
        {
            translateBySpeed += value;
        }

        public void TranslateLocalXBy(float value)
        {
            translateByValue.x = value;
            translateSpace = Space.Self;
        }

        public void TranslateLocalYBy(float value)
        {
            translateByValue.y = value;
            translateSpace = Space.Self;
        }

        public void TranslateLocalZBy(float value)
        {
            translateByValue.z = value;
            translateSpace = Space.Self;
        }

        public void TranslateGlobalXBy(float value)
        {
            translateByValue.x = value;
            translateSpace = Space.World;
        }

        public void TranslateGlobalYBy(float value)
        {
            translateByValue.y = value;
            translateSpace = Space.World;
        }

        public void TranslateGlobalZBy(float value)
        {
            translateByValue.z = value;
            translateSpace = Space.World;
        }
        #endregion

        #region Rotate Methods
        public void XRotationEquals(float value)
        {
            rotatingValue.Set(value * rotatingModifier, transform.localEulerAngles.y, transform.localEulerAngles.z);
            transform.localEulerAngles = rotatingValue; 
        }

        public void YRotationEquals(float value)
        {
            rotatingValue.Set(transform.localEulerAngles.x, value * rotatingModifier, transform.localEulerAngles.z);
            transform.localEulerAngles = rotatingValue;
        }

        public void ZRotationEquals(float value)
        {
            rotatingValue.Set( transform.localEulerAngles.x, transform.localEulerAngles.y, value * rotatingModifier);
            transform.localEulerAngles = rotatingValue;
        }

        public void ModifyRotateSpeedBy(float value)
        {
            rotateBySpeed += value;
        }

        public void RotateLocalXBy(float value)
        {
            rotateByValue.x = value;
            rotateSpace = Space.Self;
        }

        public void RotateLocalYBy(float value)
        {
            rotateByValue.y = value;
            rotateSpace = Space.Self;
        }

        public void RotateLocalZBy(float value)
        {
            rotateByValue.z = value;
            rotateSpace = Space.Self;
        }

        public void RotateGlobalXBy(float value)
        {
            rotateByValue.x = value;
            rotateSpace = Space.World;
        }

        public void RotateGlobalYBy(float value)
        {
            rotateByValue.y = value;
            rotateSpace = Space.World;
        }

        public void RotateGlobalZBy(float value)
        {
            rotateByValue.z = value;
            rotateSpace = Space.World;
        }
        #endregion

        private void FixedUpdate()
        {
            if (translateByValue != Vector3.zero)
            {
                transform.Translate(translateByValue * translateBySpeed * Time.fixedDeltaTime, translateSpace);
                SimpleHelper.LogEvent($"Translated by x:{translateByValue.x} y: {translateByValue.y} z: {translateByValue.z}", debug);
            }

            if (rotateByValue != Vector3.zero)
            {
                transform.Rotate(rotateByValue * rotateBySpeed * Time.fixedDeltaTime, rotateSpace);
                SimpleHelper.LogEvent($"Rotated by x:{rotateByValue.x} y: {rotateByValue.y} z: {rotateByValue.z}", debug);
            }
        }
    }
}
