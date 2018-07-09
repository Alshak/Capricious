using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps
{
    public class Swinging : MonoBehaviour
    {
        public float delta = 1.5f;  // Amount to move left and right from the start point
        public float speed = 2.0f;
        public float direction = 1;
        private Quaternion startPos;
        public bool IsActivated = true;

        void Start()
        {
            startPos = transform.rotation;
        }

        void Update()
        {
            if (IsActivated)
            {
                Quaternion a = startPos;
                a.z += direction * (delta * Mathf.Sin(Time.time * speed));
                transform.rotation = a;
            }
        }
    }
}
