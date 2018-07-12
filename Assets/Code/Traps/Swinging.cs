using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps
{
    public class Swinging : MonoBehaviour
    {
        Rigidbody2D body2d;
        public float leftPushRange;
        public float rightPushRange;
        public float velocityThreshold;
        void Start()
        {
            body2d = GetComponent<Rigidbody2D>();
            body2d.angularVelocity = velocityThreshold;
        }

        void Update()
        {
            if(transform.rotation.z > 0 && transform.rotation.z < rightPushRange && body2d.angularVelocity > 0 && body2d.angularVelocity < velocityThreshold)
            {
                body2d.angularVelocity = velocityThreshold;
            }
            if (transform.rotation.z < 0 && transform.rotation.z > leftPushRange && body2d.angularVelocity < 0 && body2d.angularVelocity > velocityThreshold * -1)
            {
                body2d.angularVelocity = velocityThreshold * -1;
            }
        }
    }
}
