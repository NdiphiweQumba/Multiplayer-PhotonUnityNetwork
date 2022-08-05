using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleTools;

namespace SimpleTools.Tools
{
    [DisallowMultipleComponent, ExecuteAlways, AddComponentMenu("SimpleTools/Tools/Value Inspector")]
    public class ValueInpector : MonoBehaviour
    {
        private Vector3 currentPosition, storedPosition;
        private float storedVelocity;

        [SerializeField]
        private SpaceType spaceType;
        [SerializeField]
        private Vector3 position, rotation;
        [Space(18f)]
        [SerializeField]
        private float velocity;
        [SerializeField]
        private float acceleration;

        public void Update()
        {
            position = spaceType == SpaceType.Local ? transform.localPosition : transform.position;
            rotation = spaceType == SpaceType.Local ? transform.localEulerAngles : transform.eulerAngles;
        }

        public void FixedUpdate()
        {
            currentPosition = transform.position;

            velocity = Vector3.Distance(currentPosition, storedPosition) / (Time.fixedDeltaTime / 1) ;
            acceleration = (velocity - storedVelocity) / (Time.fixedDeltaTime / 1);
            storedVelocity = velocity;
            storedPosition = transform.position;
        }
    }
}