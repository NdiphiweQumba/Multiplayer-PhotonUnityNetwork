using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SimpleTools.SpecificManagers
{
    [RequireComponent(typeof(Collider)), AddComponentMenu("SimpleTools/Specific Managers/On Trigger Manager")]
    public class OnTriggerManager : MonoBehaviour
    {
        [Serializable]
        private struct EventList
        {
            public UnityEvent<GameObject> OnTriggerEnter, OnTriggerStay, OnTriggerExit;
        }
        
        private GameObject otherObject;
        private (bool entered, bool staying, bool exited) triggered;

        [SerializeField, TagList]
        private string Tag;
        [SerializeField]
        private EventList eventList;
        [SerializeField]
        private bool triggerOnce;

        private void OnTriggerEnter(Collider other)
        {
            if (triggerOnce && triggered.entered) return;
            if (other.CompareTag(Tag))
            {
                triggered.entered = true;
                otherObject = other.gameObject;
                eventList.OnTriggerEnter.Invoke(otherObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (triggerOnce && triggered.staying) return;
            if (other.CompareTag(Tag) && otherObject)
            {
                triggered.staying = true;
                eventList.OnTriggerStay.Invoke(otherObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (triggerOnce && triggered.exited) return;
            if (other.CompareTag(Tag) && otherObject)
            {
                triggered.exited = true;
                eventList.OnTriggerExit.Invoke(otherObject);
            }
        }
    }
}
