using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleTools.ComponentManagers
{
    [RequireComponent(typeof(Rigidbody)), DisallowMultipleComponent, AddComponentMenu("SimpleTools/Component Managers/Rigidbody Manager")]
    public class RigidbodyManager : MonoBehaviour
    {
        private Rigidbody rb;
        private Vector3 force, relativeForce, torque, relativeTorque;

        [SerializeField]
        private ForceMode forceMode;
        [SerializeField]
        private float forceMultiplier = 1;

        // Start is called before the first frame update
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void AddGlobalForceX(float value)
        {
            force.x = value;
        }

        public void AddGlobalForceY(float value)
        {
            force.y = value;
        }

        public void AddGlobalForceZ(float value)
        {
            force.z = value;
        }

        public void AddLocalForceX(float value)
        {
            relativeForce.x = value;
        }

        public void AddLocalForceY(float value)
        {
            relativeForce.y = value;
        }

        public void AddLocalForceZ(float value)
        {
            relativeForce.z = value;
        }

        public void AddGlobalTorqueX(float value)
        {
            torque.x = value;
        }

        public void AddGlobalTorqueY(float value)
        {
            torque.y = value;
        }

        public void AddGlobalTorqueZ(float value)
        {
            torque.z = value;
        }

        public void AddLocalTorqueX(float value)
        {
            relativeTorque.x = value;
        }

        public void AddLocalTorqueY(float value)
        {
            relativeTorque.y = value;
        }

        public void AddLocalTorqueZ(float value)
        {
            relativeTorque.z = value;
        }

        private void FixedUpdate()
        {
            if (force != Vector3.zero) rb.AddForce(force * forceMultiplier, forceMode);
            if (relativeForce != Vector3.zero) rb.AddRelativeForce(relativeForce * forceMultiplier, forceMode);

            if (torque != Vector3.zero) rb.AddTorque(torque, forceMode);
            if (relativeTorque != Vector3.zero) rb.AddRelativeTorque(relativeTorque, forceMode);
        }
    }
}
